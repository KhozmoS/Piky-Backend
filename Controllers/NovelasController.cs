using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mysqlefcore;
using PikyServer.Models;

namespace PikyServer.Controllers {
    [Route("api/[controller]")]
    public class NovelasController : Controller {
        private PikyContext _context;
        public NovelasController() {
            _context = PikyContext.PikyContextFactory.Create();
        }

        // api/novelas GET
        [HttpGet]
        public ActionResult Get() {
            // int[] a = new int [2];
            // a[-1] = 1;
            return Ok(this._context.Novela.ToArray());
        }
        
        // api/novelas/id GET
        [HttpGet("{id}")]
        public ActionResult Get( int id ) {
            var target = this._context.Novela.FirstOrDefault( p => p.Novela_Id==id );
            if( target == null ) {
                return NotFound();
            }
            return Ok( target );
        }

        // api/novelas POST
        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Post( [FromBody]Novela novela ) {
            if( ModelState.IsValid ) {
               this._context.Novela.Add(novela);
               this._context.SaveChanges();
               return Created( $"api/peliculas/{novela.Novela_Id}", novela);
            }
            return BadRequest();
        }

        // api/novelas/id PUT
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public ActionResult Put( int id , [FromBody]Novela novela ) {
           var target = this._context.Novela.FirstOrDefault( p => p.Novela_Id == id );
           if( target==null ) {
               return NotFound();
           } else if( !ModelState.IsValid ) {
               return BadRequest();
           } else {
                this._context.Entry(target).CurrentValues.SetValues(novela);
                this._context.SaveChanges();
                return Ok();
           }
        }

        // api/novelas/id DELETE
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public ActionResult Delete( int id ) {
            var target = this._context.Novela.FirstOrDefault( p => p.Novela_Id == id );
            if( target != null ) {
                this._context.Novela.Remove( target );
                this._context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

    }
}