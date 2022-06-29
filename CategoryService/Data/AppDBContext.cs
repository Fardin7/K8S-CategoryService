using Microsoft.EntityFrameworkCore;
using CategoryService.Model;
namespace CategoryService.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions option) : base(option)
        {

        }
        public DbSet<NewsCategory> NewsCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("start seeding.......");
            modelBuilder.Entity<NewsCategory>();
            modelBuilder.Entity<NewsCategory>().HasData(
                new NewsCategory()
                {
                    Id = 1,
                    Name = "Sport",
                    Description = "this category is about Sport"
                }
                ,
                 new NewsCategory()
                 {
                     Id = 2,
                     Name = "Art",
                     Description = "this category is about Art"
                 },
                  new NewsCategory()
                  {
                      Id = 3,
                      Name = "Technology",
                      Description = "this category is about Technology"
                  });

            base.OnModelCreating(modelBuilder);
        }
    }
}
