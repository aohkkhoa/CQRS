using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.EntityTest;

[Table("TestSeeding")]
public class Test
{
    public int Id { get; set; }
    public string Name { get; set; }
}