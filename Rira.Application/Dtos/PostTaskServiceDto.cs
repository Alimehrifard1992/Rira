using System.ComponentModel.DataAnnotations;

namespace Rira.Application.Dtos
{
    public class PostTaskServiceDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
