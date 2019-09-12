using System.ComponentModel.DataAnnotations;

namespace PikyServer.Models {

    public class Pelicula  {
        [Key]
        public int Pelicula_Id { get; set; }
        [Required]
        public string Pelicula_Nombre { get; set; }
        public string Pelicula_Genero { get; set; }
        public string Pelicula_Sipopsis { get; set; }
        public int Pelicula_Publicacion { get; set; }
        public string Pelicula_Pais { get; set; }             
    }
}