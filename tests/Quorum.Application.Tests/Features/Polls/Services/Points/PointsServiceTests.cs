using FluentAssertions;
using NSubstitute;
using Quorum.Application.Features.Polls.Services.Points;
using Quorum.Application.Interfaces;
using Quorum.Domain.Entities;
using Quorum.Domain.Repositories;

namespace Quorum.Application.Tests.Features.Polls.Services.Points;

public class PointsServiceTests
{
    private readonly IPollRepository _pollRepository = Substitute.For<IPollRepository>();
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly PointsService _sut;

    public PointsServiceTests()
    {
        _sut = new PointsService(_pollRepository, _userRepository, _unitOfWork);
    }

    private static Poll CreatePollWithOption(out Guid optionId, int voteGoal = 3)
    {
        var poll = new Poll("Best Language", "Vote for the best language", Guid.NewGuid(), voteGoal);
        poll.AddOption("C#");
        optionId = poll.Options.Single().Id;
        return poll;
    }

    private static User CreateUser(string name = "Alice") =>
        new(name, $"{name.ToLowerInvariant()}@test.com", "hashed-password");

    [Fact]
    public async Task AwardAsync_WhenPollDoesNotExist_ShouldNotAwardPointsOrCommit()
    {
        var pollId = Guid.NewGuid();
        _pollRepository.GetByIdAsync(pollId).Returns((Poll?)null);

        await _sut.AwardAsync(pollId, Guid.NewGuid());

        await _userRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
        await _unitOfWork.DidNotReceive().CommitAsync();
    }

    [Fact]
    public async Task AwardAsync_WhenOptionDoesNotBelongToPoll_ShouldNotAwardPointsOrCommit()
    {
        var poll = CreatePollWithOption(out _);
        _pollRepository.GetByIdAsync(poll.Id).Returns(poll);

        // optionId aleatório, que não existe na poll
        await _sut.AwardAsync(poll.Id, Guid.NewGuid());

        await _userRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
        await _unitOfWork.DidNotReceive().CommitAsync();
    }

    [Fact]
    public async Task AwardAsync_WithPredictionsOnWinningOption_ShouldAward100PointsToEachPredictingUser()
    {
        var poll = CreatePollWithOption(out var optionId);
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        poll.AddPrediction(optionId, userId1);
        poll.AddPrediction(optionId, userId2);

        var user1 = CreateUser("Alice");
        var user2 = CreateUser("Bob");

        _pollRepository.GetByIdAsync(poll.Id).Returns(poll);
        _userRepository.GetByIdAsync(userId1).Returns(user1);
        _userRepository.GetByIdAsync(userId2).Returns(user2);

        await _sut.AwardAsync(poll.Id, optionId);

        user1.PredictionPoints.Should().Be(100);
        user2.PredictionPoints.Should().Be(100);
        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public async Task AwardAsync_WhenWinningOptionHasNoPredictions_ShouldCommitWithoutAwardingAnyone()
    {
        var poll = CreatePollWithOption(out var optionId);
        _pollRepository.GetByIdAsync(poll.Id).Returns(poll);

        await _sut.AwardAsync(poll.Id, optionId);

        await _userRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public async Task AwardAsync_WhenPredictingUserNoLongerExists_ShouldSkipThatUserWithoutThrowing()
    {
        var poll = CreatePollWithOption(out var optionId);
        var missingUserId = Guid.NewGuid();
        poll.AddPrediction(optionId, missingUserId);

        _pollRepository.GetByIdAsync(poll.Id).Returns(poll);
        _userRepository.GetByIdAsync(missingUserId).Returns((User?)null);

        var act = async () => await _sut.AwardAsync(poll.Id, optionId);

        await act.Should().NotThrowAsync();
        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public async Task AwardAsync_WithMultiplePredictionsOnDifferentOptions_ShouldOnlyAwardUsersOnWinningOption()
    {
        var poll = CreatePollWithOption(out var winningOptionId);
        poll.AddOption("F#");
        var losingOptionId = poll.Options.Single(o => o.Id != winningOptionId).Id;

        var winningUserId = Guid.NewGuid();
        var losingUserId = Guid.NewGuid();
        poll.AddPrediction(winningOptionId, winningUserId);
        poll.AddPrediction(losingOptionId, losingUserId);

        var winningUser = CreateUser("Winner");
        _pollRepository.GetByIdAsync(poll.Id).Returns(poll);
        _userRepository.GetByIdAsync(winningUserId).Returns(winningUser);

        await _sut.AwardAsync(poll.Id, winningOptionId);

        winningUser.PredictionPoints.Should().Be(100);
        await _userRepository.DidNotReceive().GetByIdAsync(losingUserId);
        await _unitOfWork.Received(1).CommitAsync();
    }
}