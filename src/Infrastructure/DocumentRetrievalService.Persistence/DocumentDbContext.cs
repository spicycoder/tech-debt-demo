using DocumentRetrievalService.Application.Common.Interfaces;
using DocumentRetrievalService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentRetrievalService.Persistence;

public class DocumentDbContext : DbContext, IDocumentDbContext
{
    public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
    {
    }

    public DbSet<Document> Documents => Set<Document>();

    IQueryable<Document> IDocumentDbContext.Documents => Documents;

    public void AddRange(IEnumerable<Document> entities)
    {
        base.AddRange(entities);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Category).HasMaxLength(50);
        });
    }
}
