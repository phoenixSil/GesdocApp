using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Features.Dtos.Fichiers.Validators
{
    public class FileValidator: AbstractValidator<IFileDto>
    {
        public FileValidator()
        {
            // pas de validation a faire ici 
        }
    }
}
