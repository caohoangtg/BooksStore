using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Marks
    {
        [Key]
        [Column(Order = 0)]
        public int PostId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }
        public virtual Posts Post { get; set; }
        public virtual Users User { get; set; }
    }
}