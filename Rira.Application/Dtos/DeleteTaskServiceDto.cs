using System.ComponentModel.DataAnnotations;

namespace Rira.Application.Dtos
{
    public class DeleteTaskServiceDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
