using ECommerce.Application.Features.Commands.Product.CreateProduct;
using FluentValidation;

namespace ECommerce.Application.Validators.Products;

public class CreateProductValidator:AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Lütfen ürün adını boş geçmeyiniz.");
        RuleFor(c => c.Name).NotNull().WithMessage("Lütfen ürün adını boş geçmeyiniz.");
        RuleFor(c => c.Name)
            .MaximumLength(150)
            .MinimumLength(5)   
                .WithMessage("Lütfen ürün adını 5 ile 150 karakter arasında giriniz.");
        
        
        RuleFor(c => c.Stock)
            .NotEmpty()
            .NotNull()
            .WithMessage("Lütfen stok bilgisini boş geçmeyiniz.");
        RuleFor(c => c.Stock)
            .Must(c => c >= 0)
            .WithMessage("Stok bilgisi negatif olamaz!");
        
        
        RuleFor(c => c.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz.");
        RuleFor(c => c.Price)
            .Must(c => c >= 0)
            .WithMessage("Fiyat bilgisi negatif olamaz!");

    }
}