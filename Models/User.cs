using System.ComponentModel.DataAnnotations;

namespace PikyServer.Models
{

    public class User
    {
        [Key]
        public int User_Id { get; set; }
        
        public string User_Name { get; set; }
        
        public string User_FirstName { get; set; }
        
        public string User_LastName { get; set; }
        
        public string User_Password { get; set; }
        
        public string User_Role { get; set; }
        public string User_Img { get; set; }        
    }
}