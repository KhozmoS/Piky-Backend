using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mysqlefcore;
using PikyServer.Models;

namespace PikyServer.Controllers {
    [Route("api/[controller]")]
    public class PeliculasController : Controller {

        private PikyContext _context;
        public PeliculasController() {
            this._context = new PikyContext();
        }

        // api/peliculas GET
        [HttpGet]
        public ActionResult Get() {
            return Ok(this._context.Pelicula.ToArray());
        }
        
        // api/peliculas/id GET
        [HttpGet("{id}")]
        public ActionResult Get( int id ) {
            var target = this._context.Pelicula.FirstOrDefault( p => p.Pelicula_Id==id );
            if( target == null ) {
                return NotFound();
            }
            return Ok( target );
        }

        // api/peliculas POST
        [HttpPost]
        public ActionResult Post( [FromBody]Pelicula pelicula ) {
            if( ModelState.IsValid ) {                
               this._context.Pelicula.Add(pelicula);
               this._context.SaveChanges();
               return Created( $"api/peliculas/{pelicula.Pelicula_Id}", pelicula);
            }
           // System.Console.WriteLine("11");
            return BadRequest();
        }

        // api/peliculas/id PUT
        [HttpPut("{id}")]
        public ActionResult Put( int id , [FromBody]Pelicula pelicula ) {
           var target = this._context.Pelicula.FirstOrDefault( p => p.Pelicula_Id == id );
           if( target==null ) {
               return NotFound();
           } else if( !ModelState.IsValid ) {
               return BadRequest();
           } else {
                this._context.Entry(target).CurrentValues.SetValues(pelicula);
                this._context.SaveChanges();
                return Ok();
           }
        }

        // api/peliculas/id DELETE
        [HttpDelete("{id}")]
        public ActionResult Delete( int id ) {
            var target = this._context.Pelicula.FirstOrDefault( p => p.Pelicula_Id == id );
            if( target != null ) {
                this._context.Pelicula.Remove( target );
                this._context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

    }
}