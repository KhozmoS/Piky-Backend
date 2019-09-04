using System.ComponentModel.DataAnnotations;

namespace PikyServer.Models {

    public class Serie {
        [Key]
        public int Serie_Id { get; set; }
        public string Serie_Nombre { get; set; }
        public string Serie_Genero { get; set; }
        public string Serie_Sipopsis { get; set; }
        public int Serie_Publicacion { get; set; }
        public string Serie_Pais { get; set; }     
        public int Serie_CantidadCapitulos { get; set; }
    }
}