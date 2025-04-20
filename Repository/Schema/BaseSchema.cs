using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCleanArch.Repository.Schema;

public abstract class BaseSchema<I, O>
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    public virtual void Update(I entity)
    {
        UpdatedAt = DateTime.Now;
    }

    public abstract O GetEntity();
}