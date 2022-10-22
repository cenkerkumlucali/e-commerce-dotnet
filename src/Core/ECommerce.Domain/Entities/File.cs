using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Entities.Common;

namespace ECommerce.Domain.Entities;

public class File : BaseEntity
{
    public string FileName { get; set; }
    public string Path { get; set; }
    [NotMapped]
    public override DateTime UpdatedDate { get; set; }
}