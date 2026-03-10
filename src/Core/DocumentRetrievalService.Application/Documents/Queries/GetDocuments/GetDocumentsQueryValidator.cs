using FluentValidation;

namespace DocumentRetrievalService.Application.Documents.Queries.GetDocuments;

public class GetDocumentsQueryValidator : AbstractValidator<GetDocumentsQuery>
{
    public GetDocumentsQueryValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThan(0)
            .WithMessage("Count must be greater than 0.");
    }
}
