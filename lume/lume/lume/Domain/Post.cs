using System;
using System.Collections.Generic;
using System.Text;

namespace lume.Domain
{
    public class Post
    {
        public string UserFullName { set; get; }
        
        public string ImageUrl { get; set; }

        public int Number { get; set; }

        public string Description { set; get; }

        public Post()
        {

        }
    }
}
