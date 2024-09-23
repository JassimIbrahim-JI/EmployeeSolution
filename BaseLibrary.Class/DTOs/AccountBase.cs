

using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Class.DTOs
{
    public class AccountBase
    {
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required, DataType(DataType.Password), MinLength(8)]
        public string? Password { get; set; }
    }
}
