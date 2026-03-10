using System.Globalization;
using System.IO;
using CsvHelper;
using DocumentRetrievalService.Application.Common.Interfaces;

namespace DocumentRetrievalService.Infrastructure.Services;

public class CsvExporter : ICsvExporter
{
    public byte[] ExportDocumentsToCsv<T>(IEnumerable<T> records)
    {
        using var memoryStream = new MemoryStream();
        using (var writer = new StreamWriter(memoryStream))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
