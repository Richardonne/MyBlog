using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.ViewModels
{
    public class UpdatePostModel
    {
        public string Title { get; set; }
        
        public string Content { get; set; }
    }
}
