namespace DocumentRetrievalService.Application.Documents.Queries.ExportDocuments;

public record ExportDocumentsFileResponse(byte[] Content, string FileName, string ContentType);
