using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Class.Entities
{
    public class UserProfile
    {
        [Required]
        public string Id { get; set; } = string.Empty;
            //Guid.NewGuid().ToString();
        [Required] public string Name { get; set; } = string.Empty;
        
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        public string ProfilePicture { get; set;} = "../images/Profile/Profile.jpg"; 


    }
}
