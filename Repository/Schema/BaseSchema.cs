using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCleanArch.Repository.Schema;

public abstract class BaseSchema<I, O>
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public abstract void Update(I entity);
    public abstract O GetEntity();
}