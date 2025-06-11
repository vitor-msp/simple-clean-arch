using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCleanArch.Api.Controllers;

[ApiController]
[Route("test")]
public class TestController() : ControllerBase
{
    public class Input
    {
        [AllowedValues(new object?[] { "clothing", "eletronics" })]
        public required string Category { get; set; }

        [RegularExpression(pattern: @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
        public required string Password { get; set; }

        [Compare("Password")]
        public required string PasswordConfirmation { get; set; }

        [EmailAddress]
        [MinLength(5)]
        [MaxLength(10)]
        public required string Email { get; set; }

        private readonly OperationType _operationType = TestController.OperationType.Credit;
        [Required]
        public required string OperationType
        {
            get => _operationType.ToString();
            init { _operationType = Enum.Parse<OperationType>(value, ignoreCase: true); }
        }

        [Phone]
        public required string Phone { get; set; }

        [Range(minimum: 18, maximum: 65)]
        public required int Age { get; set; }

        [Required]
        [Length(minimumLength: 5, maximumLength: 10)]
        public required string Address { get; set; }

        [StringLength(maximumLength: 10, MinimumLength = 5)]
        public required string AddressLine2 { get; set; }

        [Url]
        public required string PersonalWebsiteUrl { get; set; }
    }

    public enum OperationType
    {
        Credit = 10,
        Debit = 20,
        Transfer = 30
    }

    private static Input? LastInput { get; set; }

    [HttpPost("{id:int}")]
    public async Task<ActionResult> PostTest([FromBody] Input input, [FromRoute] int id,
        [FromHeader] string? apiKey, [FromQuery] bool deleted = true)
    {
        LastInput = input;
        return CreatedAtRoute("GetTest", new { id }, new
        {
            input,
            ApiKey = apiKey,
            Id = id,
            Deleted = deleted
        });
    }

    [HttpGet("{id}", Name = "GetTest")]
    public async Task<ActionResult<Input?>> GetTest(string id)
    {
        Response.Headers.ETag = Guid.NewGuid().ToString();
        return Ok(LastInput);
    }
}