using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<AppUser>
    {
        public WriterValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Yazar adı boş geçilemez.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Yazar maili boş geçilemez.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Yazar resmi boş geçilemez.");
            RuleFor(x => x.NameSurname).MinimumLength(2).WithMessage("Lütfen en az 2 karakter giriniz.");
            RuleFor(x => x.NameSurname).MaximumLength(50).WithMessage("En fazla 50 karakter giriniz.");
            RuleFor(p => p.Email).Matches(@"[@,.]+").WithMessage("Mail adresi @ ve . icermelidir");
           

        }
    }
}
