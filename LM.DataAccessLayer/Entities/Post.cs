using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.DataAccessLayer.Entities
{
    public class Post
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string postId { get; set; }
        [Required]
        public string text { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string role { get; set; }
        [Required]
        public string type { get; set; }
    }
}
