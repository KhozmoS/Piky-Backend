using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mysqlefcore;

namespace PikyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private PikyContext _context;
        public UserController()
        {
            _context = PikyContext.PikyContextFactory.Create();
        }
        // GET: api/user
        [HttpGet]
        public ActionResult Get( [FromHeader]string name )
        {
            var user = _context.User.FirstOrDefault(u => u.User_Name == name);
            if (user != null)
                return Ok(user);
            return BadRequest();
        }

        
    }
}
