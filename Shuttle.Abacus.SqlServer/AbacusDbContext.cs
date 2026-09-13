using Microsoft.EntityFrameworkCore;

namespace Shuttle.Abacus.SqlServer;

public class AbacusDbContext(DbContextOptions<AbacusDbContext> options) : DbContext(options)
{
    public DbSet<Models.Argument> Arguments { get; set; } = null!;
    public DbSet<Models.ArgumentValue> ArgumentValues { get; set; } = null!;
    public DbSet<Models.Algorithm> Algorithms { get; set; } = null!;
    public DbSet<Models.AlgorithmOperation> AlgorithmOperations { get; set; } = null!;
    public DbSet<Models.AlgorithmConstraint> AlgorithmConstraints { get; set; } = null!;
    public DbSet<Models.Matrix> Matrices { get; set; } = null!;
    public DbSet<Models.MatrixConstraint> MatrixConstraints { get; set; } = null!;
    public DbSet<Models.MatrixElement> MatrixElements { get; set; } = null!;
    public DbSet<Models.Test> Tests { get; set; } = null!;
    public DbSet<Models.TestArgument> TestArguments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Argument>()
            .HasMany(p => p.Values)
            .WithOne(f => f.Argument)
            .HasForeignKey(f => f.ArgumentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Models.Algorithm>()
            .HasMany(p => p.Operations)
            .WithOne(f => f.Algorithm)
            .HasForeignKey(f => f.AlgorithmId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Models.Algorithm>()
            .HasMany(p => p.Constraints)
            .WithOne(f => f.Algorithm)
            .HasForeignKey(f => f.AlgorithmId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Models.Matrix>()
            .HasMany(p => p.Constraints)
            .WithOne(f => f.Matrix)
            .HasForeignKey(f => f.MatrixId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Models.Matrix>()
            .HasMany(p => p.Elements)
            .WithOne(f => f.Matrix)
            .HasForeignKey(f => f.MatrixId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Models.Test>()
            .HasMany(p => p.Arguments)
            .WithOne(f => f.Test)
            .HasForeignKey(f => f.TestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
