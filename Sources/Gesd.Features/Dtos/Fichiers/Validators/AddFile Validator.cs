using FluentValidation;

namespace Gesd.Features.Dtos.Fichiers.Validators
{
    public class AddFile_Validator : AbstractValidator<FileToAddDto>
    {
        public AddFile_Validator()
        {
            Include(new FileValidator());
        }
    }
}
