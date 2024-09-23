using System.ComponentModel.DataAnnotations;


namespace BaseLibrary.Class.DTOs
{
    public class Register : AccountBase
    {
        [Required,MinLength(5), MaxLength(100)]
        public string? FullName { get; set; }

        [Required,MinLength(8), DataType(DataType.Password)]
        [Compare(nameof(Password))]

        public string? ConfirmPassword { get; set; }
    }
}
