using DocumentRetrievalService.Application.Common.Interfaces;
using MediatR;

namespace DocumentRetrievalService.Application.Documents.Queries.ExportDocuments;

public class ExportDocumentsQueryHandler : IRequestHandler<ExportDocumentsQuery, ExportDocumentsFileResponse>
{
    private readonly IDocumentDbContext _context;
    private readonly ICsvExporter _csvExporter;

    public ExportDocumentsQueryHandler(IDocumentDbContext context, ICsvExporter csvExporter)
    {
        _context = context;
        _csvExporter = csvExporter;
    }

    public Task<ExportDocumentsFileResponse> Handle(ExportDocumentsQuery request, CancellationToken cancellationToken)
    {
        var documents = _context.Documents
            .Where(x => x.Category == request.Category)
            .ToList();

        var fileContent = _csvExporter.ExportDocumentsToCsv(documents);

        return Task.FromResult(new ExportDocumentsFileResponse(
            fileContent,
            $"{request.Category}_documents.csv",
            "text/csv"));
    }
}
