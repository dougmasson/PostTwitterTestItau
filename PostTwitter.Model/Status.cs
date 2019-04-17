using System.ComponentModel.DataAnnotations;

namespace PostTwitter.Model
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}
