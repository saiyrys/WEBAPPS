using Markets.Contracts.Requests.UserRequest;

namespace Markets.Abstractions;

public interface IAuthService
{
    Task<IResult> Login(UserAuthRequest request);
    Task<IResult> Register(UserAuthRequest request);
}
