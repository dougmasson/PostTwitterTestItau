using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostTwitter.Model
{
    [Table("hashTag")]
    public class HashTag
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }

        public int idStatus { get; set; }

        [ForeignKey("idStatus")]
        public virtual Status Status { get; set; }
    }
}
