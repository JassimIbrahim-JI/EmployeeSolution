

using System.ComponentModel.DataAnnotations;


namespace BaseLibrary.Class.Entities
{
    public class Employee : BaseEntity
    {
     [Required] public string CivilId { get; set; } = string.Empty;
     [Required] public string FileNumber { get; set; } = string.Empty;
     [Required] public string JobName { get; set; } = string.Empty;
     [Required] public string Address { get; set; } = string.Empty;
     [Required] public string TelephoneNumber { get; set; } = string.Empty;
     [Required] public string PhotoUrl { get; set; } = string.Empty;
     public string? Other { get; set; } 

     //Relationship : Many to one with branch and town
    
     public Branch? branch { get; set; }
     public int BranchId { get; set; }
     public Town? town { get; set; }
     public int TownId { get; set; }

    }
}
