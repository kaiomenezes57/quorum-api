namespace Quorum.Application.Interfaces;

public interface ITokenService
{
    string Generate(Guid userId);
}