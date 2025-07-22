using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DHAFacilitationAPIs.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }

}
