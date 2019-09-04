using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mysqlefcore;
using PikyServer.Models;

namespace PikyServer.Controllers {
    [Route("api/[controller]")]
    public class RealitiesController : Controller {

        private PikyContext _context;
        public RealitiesController() {
            _context = new PikyContext();
        }

        // api/realities GET
        [HttpGet]
        public ActionResult Get() {
            return Ok(this._context.Reality.ToArray());
        }
        
        // api/realities/id GET
        [HttpGet("{id}")]
        public ActionResult Get( int id ) {
            var target = this._context.Reality.FirstOrDefault( p => p.Reality_Id==id );
            if( target == null ) {
                return NotFound();
            }
            return Ok( target );
        }

        // api/realities POST
        [HttpPost]
        public ActionResult Post( [FromBody]Reality reality ) {
            if( ModelState.IsValid ) {                
               this._context.Reality.Add(reality);
               this._context.SaveChanges();
               return Created( $"api/peliculas/{reality.Reality_Id}", reality);
            }
           // System.Console.WriteLine("11");
            return BadRequest();
        }

        // api/realities/id PUT
        [HttpPut("{id}")]
        public ActionResult Put( int id , [FromBody]Reality reality ) {
           var target = this._context.Reality.FirstOrDefault( p => p.Reality_Id == id );
           if( target==null ) {
               return NotFound();
           } else if( !ModelState.IsValid ) {
               return BadRequest();
           } else {
                this._context.Entry(target).CurrentValues.SetValues(reality);
                this._context.SaveChanges();
                return Ok();
           }
        }

        // api/realities/id DELETE
        [HttpDelete("{id}")]
        public ActionResult Delete( int id ) {
            var target = this._context.Reality.FirstOrDefault( p => p.Reality_Id == id );
            if( target != null ) {
                this._context.Reality.Remove( target );
                this._context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

    }
}