using MediatR;

namespace ECommerce.Application.Features.Commands.ProductImageFile.ChangeShowCaseImage;

public class ChangeShowCaseCommandRequest:IRequest<ChangeShowCaseCommandResponse>
{
    public string ImageId { get; set; }
    public string ProductId { get; set; }
}