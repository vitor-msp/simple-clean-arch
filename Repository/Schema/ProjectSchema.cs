using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCleanArch.Repository.Schema;

[Table("projects")]
public class ProjectSchema : BaseSchema
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = "";

    public ICollection<EmployeeSchema> Employees { get; set; } = [];
}