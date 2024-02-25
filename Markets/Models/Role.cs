using System.ComponentModel.DataAnnotations;

namespace Markets.Models;

public class Role
{
    [Key] public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;  
}
