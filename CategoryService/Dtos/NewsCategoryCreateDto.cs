using System.ComponentModel.DataAnnotations;

namespace CategoryService.Dtos
{
    public class NewsCategoryCreateDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
    }
}
