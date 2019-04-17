using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostTwitter.Model
{
    public class Twitters
    {
        [Key]
        public int id { get; set; }
        public string texto { get; set; }
        public DateTime datatwitte { get; set; }
        public string idioma { get; set; }
        public string usuario { get; set; }
        public int qtdseguidores { get; set; }

        public int idHashTag { get; set; }
        public int idExecucao { get; set; }

        [ForeignKey("idHashTag")]
        public virtual HashTag HashTag { get; set; }

        [ForeignKey("idExecucao")]
        public virtual Execucao Execucao { get; set; }
    }
}
