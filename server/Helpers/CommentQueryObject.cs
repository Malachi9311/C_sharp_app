using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Helpers
{
    public class CommentQueryObject
    {
        public string? Symbol { get; set; } = string.Empty; 
        public bool IsDescending { get; set; } = true;
    }
}