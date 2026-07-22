using FluentAssertions;
using Quorum.Domain.Entities;

namespace Quorum.Domain.Tests.Entities;

public class OptionTests
{
    [Fact]
    public void AddVote_WithDuplicatedUserId_ShouldReturnFalse()
    {
        var pollId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var option = new Option("Option A", pollId);
        
        option.AddVote(userId);
        var result = option.AddVote(userId);
        
        result.Should().BeFalse();
        option.Votes.Count.Should().Be(1);
    }
}