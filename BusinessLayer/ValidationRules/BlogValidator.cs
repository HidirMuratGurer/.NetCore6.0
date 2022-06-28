using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Blog Başlığını boş geçemezsiniz");
            RuleFor(x => x.BlogContent).NotEmpty().WithMessage("Blog içeriğini boş geçemezsiniz");
            RuleFor(x => x.BlogImage).NotEmpty().WithMessage("Blog görselini boş geçemezsiniz");
            RuleFor(x => x.BlogThumbnailImage).NotEmpty().WithMessage("Blog thumbnail görselini boş geçemezsiniz");
            RuleFor(x => x.BlogTitle).MaximumLength(150).WithMessage("Lütfen 150 Karakterden az giriş yapınız.");
            RuleFor(x => x.BlogTitle).MinimumLength(5).WithMessage("Lütfen 5 Karakterden fazla giriş yapınız.");
            RuleFor(x => x.BlogContent).MinimumLength(10).WithMessage("Lütfen En az 10 Karakter girişi yapınız.");
        }
    }
}
