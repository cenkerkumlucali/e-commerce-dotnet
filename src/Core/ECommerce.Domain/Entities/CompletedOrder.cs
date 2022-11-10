using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Entities.Common;

namespace ECommerce.Domain.Entities;

public class CompletedOrder : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    [NotMapped]
    public override DateTime UpdatedDate { get; set; }
}