using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using TesteJrs.Models;

namespace TesteJrs.Controllers
{
    public class HomeController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "91meW4ruBz8qXWkCQxcu6o4PG24NT9mzqRMfbGf2",
            BasePath = "https://test-bd2c8-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;
        public async Task<ActionResult> Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetTaskAsync("Information/");
            var data = JsonConvert.DeserializeObject<List<TarefaModel>>(response.Body);
            var lista = new List<TarefaModel>();
            if(data != null)
            {
                foreach(var item in data)
                {
                    if(item != null)
                    {
                        lista.Add(item);
                    }
                }
            }
            return View(lista);
        }

        [HttpPost]
        public async Task<TarefaModel> IncluirTarefa(int id, string descricaoTarefa, DateTime dataInicio, DateTime dataFinal)
        {
            client = new FireSharp.FirebaseClient(config);

            var tarefa = new TarefaModel
            {
                Id = id,
                Descricao = descricaoTarefa,
                DataInicio = dataInicio.ToLocalTime(),
                DataFinal = dataFinal.ToLocalTime()
            };

            SetResponse response = await client.SetTaskAsync("Information/" + tarefa.Id.ToString(), tarefa);
            TarefaModel result = response.ResultAs<TarefaModel>();

            return result;
        }

        [HttpPost]
        public async Task<TarefaModel> DeletarTarefa(int id)
        {
            client = new FireSharp.FirebaseClient(config);
            DeleteResponse response = await client.DeleteTaskAsync("Information/" + id.ToString());
            TarefaModel result = new TarefaModel();
            if(response.Body != "null")
            {
                result = response.ResultAs<TarefaModel>();
            }

            return result;
        }

        [HttpGet]
        public async Task<int> RetornaUltimoId()
        {
            client = new FireSharp.FirebaseClient(config);
            
            FirebaseResponse response = await client.GetTaskAsync("Information/");
            var retorno = JsonConvert.DeserializeObject<List<TarefaModel>>(response.Body);
            var ultimoId = 0;
            if(retorno != null)
            {
                foreach (var item in retorno)
                {
                    if(item.Id > ultimoId)
                    {
                        ultimoId = item.Id;
                    }
                }
                ultimoId ++;
            }
            return ultimoId;
        }
    }
}