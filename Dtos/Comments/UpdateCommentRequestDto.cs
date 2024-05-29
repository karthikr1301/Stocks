using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comments
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5,ErrorMessage="Title must be 5 CHaracters")]
        [MaxLength(280,ErrorMessage="Title cannot be over 280 CHaracters")]
        public String Title { get; set; }=String.Empty;
        

        [Required]
        [MinLength(5,ErrorMessage="Content must be 5 CHaracters")]
        [MaxLength(280,ErrorMessage="Content cannot be over 280 CHaracters")]
        public String Content { get; set; }=String.Empty;
        
    }
}