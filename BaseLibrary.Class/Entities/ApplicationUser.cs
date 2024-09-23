
using BaseLibrary.Class.DTOs;

namespace BaseLibrary.Class.Entities
{
    public class ApplicationUser :AccountBase
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; } = string.Empty;
       
    }
}
