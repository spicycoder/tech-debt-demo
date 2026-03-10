namespace DocumentRetrievalService.Domain.Entities;

public class Document
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public long FileSize { get; set; } // in bytes
    public DateTime Created { get; set; }
}
