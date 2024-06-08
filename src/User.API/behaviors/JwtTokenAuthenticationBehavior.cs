using MediatR;
using User.API.application.service;

namespace User.API.behaviors;

public class JwtTokenAuthenticationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtTokenService _jwtTokenService;

    public JwtTokenAuthenticationBehavior(IHttpContextAccessor httpContextAccessor, IJwtTokenService jwtTokenService)
    {
        this._httpContextAccessor = httpContextAccessor;
        this._jwtTokenService = jwtTokenService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string? token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
        if (!string.IsNullOrEmpty(token) && !_jwtTokenService.ValidateJwtToken(token))
        {
            throw new UnauthorizedAccessException("トークンが無効です。");
        }

        return await next();
    }
}
