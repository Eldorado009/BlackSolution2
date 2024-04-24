using BlankSolution.Core.Entities;
using BlankSolution.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BlankSolution.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieImage> MovieImages { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(GenreConfiguration).Assembly);

		base.OnModelCreating(modelBuilder);
	}
}
