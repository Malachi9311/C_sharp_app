using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace server.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be more than 5 characters")]
        [MaxLength(300, ErrorMessage = "Title must be less than 300 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be more than 5 characters")]
        [MaxLength(300, ErrorMessage = "Content must be less than 300 characters")]
        public string Content { get; set; } = string.Empty;
    }
}