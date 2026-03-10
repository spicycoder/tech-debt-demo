using DocumentRetrievalService.Application.Common.Interfaces;
using DocumentRetrievalService.Domain.Entities;
using MediatR;

namespace DocumentRetrievalService.Application.Documents.Commands.CreateDocuments;

public class CreateDocumentsCommandHandler : IRequestHandler<CreateDocumentsCommand, int>
{
    private readonly IDocumentDbContext _context;

    public CreateDocumentsCommandHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateDocumentsCommand request, CancellationToken cancellationToken)
    {
        var entities = request.Documents.Select(d => new Document
        {
            Title = d.Title,
            FileName = d.FileName,
            Category = d.Category,
            FileSize = d.FileSize,
            Created = d.Created
        }).ToList();

        _context.AddRange(entities);
        await _context.SaveChangesAsync(cancellationToken);

        return entities.Count;
    }
}
