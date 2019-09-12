using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mysqlefcore;
using PikyServer.Models;

namespace PikyServer.Controllers {
    [Route("api/[controller]")]
    public class SeriesController : Controller {

        private PikyContext _context;
        public SeriesController() {
            this._context = PikyContext.PikyContextFactory.Create();
        }

        // api/series GET
        [HttpGet]
        public ActionResult Get() {
            return Ok(this._context.Serie.ToArray());
        }
        
        // api/series/id GET
        [HttpGet("{id}")]
        public ActionResult Get( int id ) {
            var target = this._context.Serie.FirstOrDefault( p => p.Serie_Id==id );
            if( target == null ) {
                return NotFound();
            }
            return Ok( target );
        }

        // api/series POST
        [HttpPost]
        public ActionResult Post( [FromBody]Serie serie ) {
            if( ModelState.IsValid ) {                
               this._context.Serie.Add(serie);
               this._context.SaveChanges();
               return Created( $"api/peliculas/{serie.Serie_Id}", serie);
            }
           // System.Console.WriteLine("11");
            return BadRequest();
        }

        // api/series/id PUT
        [HttpPut("{id}")]
        public ActionResult Put( int id , [FromBody]Serie serie ) {
           var target = this._context.Serie.FirstOrDefault( p => p.Serie_Id == id );
           if( target==null ) {
               return NotFound();
           } else if( !ModelState.IsValid ) {
               return BadRequest();
           } else {
                this._context.Entry(target).CurrentValues.SetValues(serie);
                this._context.SaveChanges();
                return Ok();
           }
        }

        // api/series/id DELETE
        [HttpDelete("{id}")]
        public ActionResult Delete( int id ) {
            var target = this._context.Serie.FirstOrDefault( p => p.Serie_Id == id );
            if( target != null ) {
                this._context.Serie.Remove( target );
                this._context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

    }
}