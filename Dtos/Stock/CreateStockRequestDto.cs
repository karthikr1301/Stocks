using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10,ErrorMessage="Symbol cannot be over 10 CHaracters")]
        public String  Symbol { get; set; }=String.Empty;
        

        [Required]
        [MaxLength(10,ErrorMessage="CompanyName cannot be over 10 CHaracters")]
        public String CompanyName { get; set; }=String.Empty;
        
        [Required]
        [Range(1,1000000000)]
        public decimal Purchase { get; set; }
        
        [Required]
        [Range(0.01,100)]
        public decimal LastDiv { get; set; }
        
        [Required]
        [MaxLength(10,ErrorMessage="Indusry cannot be over 10 characters")]
        public String Industry { get; set; }=String.Empty;
        
        [Required]
        [Range(1,5000000000)]
        public long MarketCap { get; set; }
        
    }
}