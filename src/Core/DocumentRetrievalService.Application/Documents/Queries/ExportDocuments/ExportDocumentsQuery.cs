using MediatR;

namespace DocumentRetrievalService.Application.Documents.Queries.ExportDocuments;

public record ExportDocumentsQuery(string Category) : IRequest<ExportDocumentsFileResponse>;
