using DocumentRetrievalService.Application.Documents.Commands.CreateDocuments;
using DocumentRetrievalService.Application.Documents.Queries.ExportDocuments;
using DocumentRetrievalService.Application.Documents.Queries.GetDocuments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentRetrievalService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{count:int}")]
    public async Task<ActionResult<List<DocumentResponse>>> Get(int count)
    {
        return await _mediator.Send(new GetDocumentsQuery(count));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateDocumentsCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet("export/{category}")]
    public async Task<FileResult> Export(string category)
    {
        var response = await _mediator.Send(new ExportDocumentsQuery(category));
        return File(response.Content, response.ContentType, response.FileName);
    }
}
