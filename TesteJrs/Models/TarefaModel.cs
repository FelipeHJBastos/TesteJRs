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
        public DateTime DataInicio{ get; set; }
        public DateTime DataFinal { get; set; }
    }
}