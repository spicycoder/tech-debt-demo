using DocumentRetrievalService.Domain.Entities;

namespace DocumentRetrievalService.Application.Common.Interfaces;

public interface IDocumentDbContext
{
    IQueryable<Document> Documents { get; }
    void AddRange(IEnumerable<Document> entities);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
