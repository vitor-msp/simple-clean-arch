using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCleanArch.Repository.Schema;

[Table("employees")]
public class EmployeeSchema : BaseSchema
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("document")]
    public string Document { get; set; } = "";

    [Column("name")]
    public string Name { get; set; } = "";

    public ICollection<ProjectSchema> Projects { get; set; } = [];
}