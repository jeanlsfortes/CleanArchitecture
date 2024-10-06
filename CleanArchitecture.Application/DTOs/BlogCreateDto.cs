using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.DTOs
{
    public record BlogCreateDto
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public required string Description { get;  set; }

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(50, ErrorMessage = "Author name cannot exceed 50 characters.")]
        public required string Author { get;  set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string? ImageUrl { get;  set; }

        public DateTime CreatedAt { get;  set; }
    }
}
