using FluentAssertions;
using Quorum.Domain.Entities;

namespace Quorum.Domain.Tests.Entities;

public class PollTests
{
    private static Poll CreateValidPoll(int voteGoal = 3) =>
        new("Best Language", "Vote for the best language", Guid.NewGuid(), voteGoal);

    [Fact]
    public void Constructor_WithValidData_ShouldCreatePoll()
    {
        var userId = Guid.NewGuid();

        var poll = new Poll("Best Language", "Vote for the best language", userId, 3);

        poll.Name.Should().Be("Best Language");
        poll.Description.Should().Be("Vote for the best language");
        poll.UserId.Should().Be(userId);
        poll.VoteGoal.Should().Be(3);
        poll.Options.Should().BeEmpty();
        poll.Id.Should().NotBe(Guid.Empty);
    }

    [Theory]
    [InlineData(null, "description")]
    [InlineData("", "description")]
    [InlineData("name", null)]
    [InlineData("name", "")]
    public void Constructor_WithMissingNameOrDescription_ShouldThrow(string? name, string? description)
    {
        var act = () => new Poll(name!, description!, Guid.NewGuid(), 3);

        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Constructor_WithVoteGoalBelowThree_ShouldThrow(int voteGoal)
    {
        var act = () => new Poll("Name", "Description", Guid.NewGuid(), voteGoal);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void UpdateInformation_WithValidData_ShouldUpdateAndReturnTrue()
    {
        var poll = CreateValidPoll();
        var lastUpdatedBefore = poll.LastUpdatedAt;

        var result = poll.UpdateInformation("New name", "New description");

        result.Should().BeTrue();
        poll.Name.Should().Be("New name");
        poll.Description.Should().Be("New description");
        poll.LastUpdatedAt.Should().BeOnOrAfter(lastUpdatedBefore);
    }

    [Theory]
    [InlineData(null, "description")]
    [InlineData("name", "")]
    public void UpdateInformation_WithMissingData_ShouldReturnFalse(string? name, string? description)
    {
        var poll = CreateValidPoll();

        var result = poll.UpdateInformation(name!, description!);

        result.Should().BeFalse();
    }

    [Fact]
    public void AddOption_WithNewName_ShouldAddAndReturnTrue()
    {
        var poll = CreateValidPoll();

        var result = poll.AddOption("C#");

        result.Should().BeTrue();
        poll.Options.Should().ContainSingle(o => o.Name == "C#");
    }

    [Fact]
    public void AddOption_WithDuplicateName_ShouldReturnFalse()
    {
        var poll = CreateValidPoll();
        poll.AddOption("C#");

        var result = poll.AddOption("C#");

        result.Should().BeFalse();
        poll.Options.Should().HaveCount(1);
    }

    [Fact]
    public void RemoveOption_WithExistingOption_ShouldRemoveAndReturnTrue()
    {
        var poll = CreateValidPoll();
        poll.AddOption("C#");
        var optionId = poll.Options.Single().Id;

        var result = poll.RemoveOption(optionId);

        result.Should().BeTrue();
        poll.Options.Should().BeEmpty();
    }

    [Fact]
    public void RemoveOption_WithUnknownId_ShouldReturnFalse()
    {
        var poll = CreateValidPoll();

        var result = poll.RemoveOption(Guid.NewGuid());

        result.Should().BeFalse();
    }

    [Fact]
    public void AddVote_ForExistingOption_ShouldReturnTrue()
    {
        var poll = CreateValidPoll();
        poll.AddOption("C#");
        var optionId = poll.Options.Single().Id;

        var result = poll.AddVote(optionId, Guid.NewGuid());

        result.Should().BeTrue();
        poll.Options.Single().Votes.Should().HaveCount(1);
    }

    [Fact]
    public void AddVote_ForUnknownOption_ShouldReturnFalse()
    {
        var poll = CreateValidPoll();

        var result = poll.AddVote(Guid.NewGuid(), Guid.NewGuid());

        result.Should().BeFalse();
    }

    [Fact]
    public void AddVote_WhenVoteGoalReached_ShouldReturnFalse()
    {
        // VoteGoal = 3: after three votes on the poll, no further votes should be accepted,
        // even for a different option.
        var poll = CreateValidPoll(voteGoal: 3);
        poll.AddOption("C#");
        var optionId = poll.Options.Single().Id;

        poll.AddVote(optionId, Guid.NewGuid());
        poll.AddVote(optionId, Guid.NewGuid());
        poll.AddVote(optionId, Guid.NewGuid());

        var result = poll.AddVote(optionId, Guid.NewGuid());

        result.Should().BeFalse();
        poll.Options.Single().Votes.Should().HaveCount(3);
    }

    [Fact]
    public void RemoveVote_ForExistingVote_ShouldReturnTrue()
    {
        var poll = CreateValidPoll();
        poll.AddOption("C#");
        var optionId = poll.Options.Single().Id;
        var userId = Guid.NewGuid();
        poll.AddVote(optionId, userId);

        var result = poll.RemoveVote(optionId, userId);

        result.Should().BeTrue();
        poll.Options.Single().Votes.Should().BeEmpty();
    }

    [Fact]
    public void RemoveVote_ForUnknownOption_ShouldReturnFalse()
    {
        var poll = CreateValidPoll();

        var result = poll.RemoveVote(Guid.NewGuid(), Guid.NewGuid());

        result.Should().BeFalse();
    }
}