using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogs.Core
{
    public class MyBlog
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(30)]
        public string Author { get; set; }
        [Required]
        public string Body { get; set; }

        
        public string? imgUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
