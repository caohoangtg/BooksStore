using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Topics
    {
        [Key]
        public int TopicId { get; set; }
        public string Name { get; set; }
        public virtual List<Posts> Posts { get; set; }
    }
}
