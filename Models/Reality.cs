using System.ComponentModel.DataAnnotations;

namespace PikyServer.Models {

    public class Reality  {
        [Key]
        public int Reality_Id { get; set; }
        [Required]
        public string Reality_Nombre { get; set; }
        public string Reality_Genero { get; set; }
        public string Reality_Sipopsis { get; set; }
        public int Reality_Publicacion { get; set; }
        public string Reality_Pais { get; set; }     
        public int Reality_CantidadCapitulos { get; set; }
        
    }
}