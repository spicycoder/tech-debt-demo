using MediatR;

namespace DocumentRetrievalService.Application.Documents.Queries.GetDocuments;

public record GetDocumentsQuery(int Count) : IRequest<List<DocumentResponse>>;
