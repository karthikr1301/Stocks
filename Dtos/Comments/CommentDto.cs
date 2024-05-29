using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comments
{
    public class CommentDto
    {
        
        public int? Id { get; set; }

        public String Title { get; set; }=String.Empty;

        public String Content { get; set; }=String.Empty;

        public DateTime CreatedOn { get; set; }=DateTime.UtcNow;

        public int StockId { get; set; }

    

    }
}