using System.ComponentModel.DataAnnotations;
namespace CategoryService.Model
{
    public class NewsCategory
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
