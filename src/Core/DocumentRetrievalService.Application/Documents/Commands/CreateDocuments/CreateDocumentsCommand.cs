using DocumentRetrievalService.Application.Documents.Queries.GetDocuments;
using MediatR;

namespace DocumentRetrievalService.Application.Documents.Commands.CreateDocuments;

public record CreateDocumentsCommand(List<DocumentResponse> Documents) : IRequest<int>;
