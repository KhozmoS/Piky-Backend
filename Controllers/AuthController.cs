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
        private IPasswordHasher _passowrdHasher;

        public AuthController(
            ITokensService tokensService,
            IPasswordHasher passwordHasher
        )
        {
            this._context = PikyContext.PikyContextFactory.Create();
            _tokensService = tokensService;
            _passowrdHasher = passwordHasher;
        }


        // POST api/auth/register
        [HttpPost("register")]
        public IActionResult Register( [FromBody]User user )
        {
            if (!ModelState.IsValid) return BadRequest();

            var exist = _context.User.FirstOrDefault(u => u.User_Name == user.User_Name);
            if( exist != null )
            {
                return BadRequest("El nombre de usuario ya existe!");
            }
            user.User_Role = "User";
            // Hashing the password and creating the tokens
            user.User_Password = _passowrdHasher.GenerateIdentityV3Hash( user.User_Password );
            string tokenAccess = _tokensService.CreateAccessToken( user );
            var newUserSession = new UserRefreshToken{
                Password = user.User_Password,
                UserName = user.User_Name,
                RefreshToken = _tokensService.CreateRefreshToken()
            };
            // Storing the data
            using( _context )
            {
                _context.User.Add( user );
                _context.UserRefreshToken.Add( newUserSession );

                _context.SaveChanges();
            }
            
            return Created("api/auth/login",new
            {
                access_token = tokenAccess,
                refresh_token = newUserSession.RefreshToken
            });
        }
        

        // GET api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid client request");
            }
            User CorrectUser = this._context.User.FirstOrDefault(
                                                                  u => (
                                                                        u.User_Name.Equals(user.UserName)
                                                                       )
                                                                );

            if (CorrectUser != null &&
                 _passowrdHasher.VerifyIdentityV3Hash(user.Password, CorrectUser.User_Password))
            {
                var tokenString = _tokensService.CreateAccessToken(CorrectUser);
                var refreshToken = _tokensService.CreateRefreshToken();

                UserRefreshToken userRefreshToken = _context.UserRefreshToken.FirstOrDefault(
                                                    u => u.UserName.Equals(user.UserName)
                    );

                userRefreshToken.RefreshToken = refreshToken;
                _context.SaveChanges();


                return Ok(new
                {
                    access_token = tokenString,
                    refresh_token = refreshToken
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST api/auth/refresh
        [HttpPost("refresh")]       
        public IActionResult RefreshToken( [FromBody]string refreshToken )
        {
            // var principal = _tokensService.GetPrincipalFromExpiredToken(authenticationToken);
            
            var userSession = _context.UserRefreshToken.SingleOrDefault(u => u.RefreshToken == refreshToken );
            
            if (userSession == null || userSession.RefreshToken != refreshToken) return BadRequest();

            var user = _context.User.FirstOrDefault( u => u.User_Name == userSession.UserName );

            var newJwtToken = _tokensService.CreateAccessToken( user );
            var newRefreshToken = _tokensService.CreateRefreshToken();

            userSession.RefreshToken = newRefreshToken;
            _context.SaveChanges();

            return new ObjectResult(new
            {
                access_token = newJwtToken,
                refresh_token = newRefreshToken
            });
        }
    }

}