using System.ComponentModel.DataAnnotations;
using SimpleCleanArch.Domain.Entities;

namespace SimpleCleanArch.Application.Dto;

public class CreateProductInput
{
    public InternalProduct Product { get; set; }

    public class InternalProduct
    {
        [Required(ErrorMessage = "description is required")]
        [MinLength(3)]
        [MaxLength(10)]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "price is required")]
        [Range(0.0, 100.0)]
        public double Price { get; set; }

        [Required(ErrorMessage = "category is required")]
        public string Category { get; set; } = "";

        public ProductFields GetFields()
        {
            return new ProductFields()
            {
                Description = Description,
                Price = Price,
                Category = Category,
            };
        }
    }
}