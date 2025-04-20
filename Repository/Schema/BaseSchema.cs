using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCleanArch.Repository.Schema;

public abstract class BaseSchema
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    protected BaseSchema()
    {
        CreatedAt = DateTime.UtcNow;
    }

    protected void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}