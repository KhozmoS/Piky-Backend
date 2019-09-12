using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using mysqlefcore;
using PikyServer.Models;
using PikyServer.Services;

namespace PikyServer.Controllers
{

    [Route("api/{controller}")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private PikyContext _context;
        private ITokensService _tokensService;        
        public AuthController( ITokensService tokensService )
        {            
            this._context = PikyContext.PikyContextFactory.Create();
            _tokensService = tokensService;
        }
        // GET api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null || !ModelState.IsValid )
            {
                return BadRequest("Invalid client request");
            }
            User CorrectUser = this._context.User.FirstOrDefault(
                                                                  u => (
                                                                        u.User_Name.Equals(user.UserName) &&
                                                                        u.User_Password.Equals(user.Password)
                                                                       )
                                                                );
            if ( CorrectUser != null )
            {                
                var tokenString = _tokensService.CreateAccessToken( CorrectUser );
                var refreshToken = _tokensService.CreateRefreshToken();

                UserRefreshToken userRefreshToken = _context.UserRefreshToken.FirstOrDefault(
                                                    u => u.UserName.Equals(user.UserName)                        
                    );                
                if( userRefreshToken == null )
                {
                    _context.UserRefreshToken.Add(new UserRefreshToken()
                    {
                        Password = user.Password,
                        UserName = user.UserName,
                        RefreshToken = refreshToken
                    });
                    _context.SaveChanges();
                }
                else
                {
                    userRefreshToken.RefreshToken = refreshToken;
                    _context.SaveChanges();
                }

                return Ok(new {
                    jwt_token = tokenString,
                    refresh_token = refreshToken
                });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
    
}