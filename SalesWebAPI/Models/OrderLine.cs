using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SalesWebAPI.Models {
    public class OrderLine {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Product { get; set; }
        [Required]
        [Column(TypeName = "decimal(7,2")]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int OrderID { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
