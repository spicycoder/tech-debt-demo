using Bogus;
using MediatR;

namespace DocumentRetrievalService.Application.Documents.Queries.GetDocuments;

public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, List<DocumentResponse>>
{
    private static readonly string[] Categories = new[] { "Legal", "Invoice", "Report", "Personal", "Archive" };

    public Task<List<DocumentResponse>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
    {
        var faker = new Faker<DocumentResponse>()
            .CustomInstantiator(f => new DocumentResponse(
                f.IndexGlobal + 1,
                f.Lorem.Sentence(3),
                f.System.FileName(),
                f.PickRandom(Categories),
                f.Random.Long(1024, 52428800), // 1KB to 50MB
                f.Date.Past(2)
            ));

        var documents = faker.Generate(request.Count);

        return Task.FromResult(documents);
    }
}
