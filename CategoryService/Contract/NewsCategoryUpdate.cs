using System.ComponentModel.DataAnnotations;

namespace CategoryService.Contract
{
    public class NewsCategoryUpdate
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
