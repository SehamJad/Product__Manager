using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.Domain;

class Product
{
    
    public int Id { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    [Column(TypeName = "nchar(12)")]
    public required string SKU { get; set; }
       
    [MaxLength(150)]
    public required string Description { get; set; }
    [MaxLength(1000)]   
    public required string Image { get; set; }

    [MaxLength(10)]    
    public required string Price { get; set; }
}