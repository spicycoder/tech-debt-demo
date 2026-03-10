namespace DocumentRetrievalService.Application.Common.Interfaces;

public interface ICsvExporter
{
    byte[] ExportDocumentsToCsv<T>(IEnumerable<T> records);
}
