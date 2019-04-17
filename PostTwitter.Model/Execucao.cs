using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostTwitter.Model
{
    public class Execucao
    {
        [Key]
        public int Id { get; set; }
        public DateTime Dataexecucao { get; set; }
        public string Usuario { get; set; }

        public int idStatus { get; set; }

        [ForeignKey("idStatus")]
        public virtual Status Status { get; set; }
    }
}
