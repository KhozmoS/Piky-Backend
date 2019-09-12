using PikyServer.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace PikyServer.Services
{
    public interface ITokensService
    {
        string CreateAccessToken( User CorrectUser );
        string CreateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}