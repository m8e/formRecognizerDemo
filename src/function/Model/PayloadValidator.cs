using FluentValidation;

namespace Contoso
{
    public class PayloadValidator : AbstractValidator<Payload>
    {
        public PayloadValidator()
        {            
            RuleFor(p => p.FileUri).NotEmpty().NotNull();
            RuleFor(p => p.ModelId).NotEmpty().NotNull();
            RuleFor(p => p.WebHook).NotEmpty().NotNull();
            RuleFor(p => p.DocumentId).NotEqual(0);
        }
    }
}