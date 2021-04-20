using System;
using System.Collections.Generic;
using System.Text;

namespace lume.Domain
{
    public class Post
    {
        public User Owner { get; set; }

        public int Number { get; set; }

        public string Description { set; get; }

        public Post()
        {

        }
    }
}
