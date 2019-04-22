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

        public override string ToString()
        {
            return string.Format("ID: {0} | Data: {1} | Usuario: {2} | Status: {3}", this.Id, this.Dataexecucao.ToString("dd/MM/yyy HH:mm"), this.Usuario, this.idStatus);
        }
    }
}
