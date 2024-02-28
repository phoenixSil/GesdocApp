using FluentValidation;

namespace Gesd.Features.Dtos.Fichiers.Validators
{
    public class FileValidator : AbstractValidator<IFileDto>
    {
        public FileValidator()
        {
            // pas de validation a faire ici 
        }
    }
}
