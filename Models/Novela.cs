using System.ComponentModel.DataAnnotations;

namespace PikyServer.Models {

    public class Novela {
        [Key]
        public int Novela_Id { get; set; }
        public string Novela_Nombre { get; set; }
        public string Novela_Genero { get; set; }
        public string Novela_Sipopsis { get; set; }
        public int Novela_Publicacion { get; set; }
        public string Novela_Pais { get; set; }     
        public int Novela_CantidadCapitulos { get; set; }
    }
}