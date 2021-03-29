using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteJrs.Models
{
    public class TarefaModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataConclusao { get; set; }
    }
}