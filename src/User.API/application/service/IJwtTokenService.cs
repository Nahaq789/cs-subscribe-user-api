using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace User.API.application.service;

/// <summary>
/// JWTトークンを生成するサービスです。
/// </summary>
public interface IJwtTokenService
{

    /// <summary>
    /// JWTトークンを復元し認証
    /// </summary>
    /// <param name="token">jwtトークン</param>
    bool ValidateJwtToken(string token);
}