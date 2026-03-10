namespace DocumentRetrievalService.Application.Documents.Queries.GetDocuments;

public record DocumentResponse(int Id, string Title, string FileName, string Category, long FileSize, DateTime Created);
