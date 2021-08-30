using DMS_Demo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Demo.Models
{
    public enum Size
    {
        size_XL = 0,
        size_L = 1,
        size_S = 2,
        size_M = 4
    }
    public enum Color 
    {
        Red = 0,
        Green = 1,
        Blue = 2,
        Yellow = 4,
        Black = 8,
        White = 16
    }

    [Table("Product")]
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_ID { get; set; }
        public int  Uom_Id { get; set; }
        [Required, MaxLength(50)]
        public string Product_Name { get; set; }
       
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Product_Price { get; set; }
        public string Images { get; set; }
        [Required]
        public Size? Product_Size { get; set; }
        public Color Product_Color { get; set; }

        [Column(TypeName = "date")]
        public DateTime Adding_Date { get; set; }
      
        public int Stored_Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }

        [ForeignKey("Uom_Id")]
        public virtual UOM Uom { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        
        
    }
}
