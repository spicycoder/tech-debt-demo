using System.Net;
using System.Net.Http.Json;
using DocumentRetrievalService.Application.Documents.Commands.CreateDocuments;
using DocumentRetrievalService.Application.Documents.Queries.GetDocuments;

namespace DocumentRetrievalService.Api.UnitTests;

public class DocumentControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DocumentControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ShouldReturnRequestedNumberOfMockDocuments()
    {
        // Act
        var response = await _client.GetAsync("/api/document/5");

        // Assert
        response.EnsureSuccessStatusCode();
        var documents = await response.Content.ReadFromJsonAsync<List<DocumentResponse>>();
        Assert.NotNull(documents);
        Assert.Equal(5, documents.Count);
    }

    [Fact]
    public async Task Get_WithZeroCount_ShouldReturnBadRequest()
    {
        // Act
        var response = await _client.GetAsync("/api/document/0");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Create_ShouldPersistDocumentsAndReturnId()
    {
        // Arrange
        var command = new CreateDocumentsCommand(new List<DocumentResponse>
        {
            new DocumentResponse(0, "Sample 1", "sample1.pdf", "Legal", 1024, DateTime.UtcNow),
            new DocumentResponse(0, "Sample 2", "sample2.pdf", "Legal", 2048, DateTime.UtcNow)
        });

        // Act
        var response = await _client.PostAsJsonAsync("/api/document", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<int>();
        Assert.True(result >= 0);
    }

    [Fact]
    public async Task Export_ShouldReturnCsvFile()
    {
        // Arrange - Seed some data
        var command = new CreateDocumentsCommand(new List<DocumentResponse>
        {
            new DocumentResponse(0, "Export Test", "export.pdf", "Finance", 512, DateTime.UtcNow)
        });
        await _client.PostAsJsonAsync("/api/document", command);

        // Act
        var response = await _client.GetAsync("/api/document/export/Finance");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/csv", response.Content.Headers.ContentType?.MediaType);
        var csvContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("Export Test", csvContent);
    }

    [Fact]
    public async Task Export_WithNonExistentCategory_ShouldReturnEmptyCsv()
    {
        // Act
        var response = await _client.GetAsync("/api/document/export/NonExistent");

        // Assert
        response.EnsureSuccessStatusCode();
        var csvContent = await response.Content.ReadAsStringAsync();
        // Should only contain headers (we can check if it's not empty but contains no data)
        Assert.NotEmpty(csvContent);
        Assert.DoesNotContain("Sample", csvContent);
    }
}
