using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Features.Dtos.Fichiers.Validators
{
    public class AddFile_Validator: AbstractValidator<FileToAddDto>
    {
        public AddFile_Validator()
        {
            Include(new FileValidator());
        }
    }
}
