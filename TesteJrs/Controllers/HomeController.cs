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
        public ActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<TarefaModel> IncluirTarefa(int id, string descricaoTarefa, DateTime dataInclusao, DateTime? dataConclusao)
        {
            client = new FireSharp.FirebaseClient(config);
            var tarefa = new TarefaModel
            {
                Id = id,
                Descricao = descricaoTarefa,
                DataInclusao = dataInclusao,
                DataConclusao = dataConclusao
            };

            SetResponse response = await client.SetTaskAsync("Information/" + tarefa.Id.ToString(), tarefa);
            TarefaModel result = response.ResultAs<TarefaModel>();

            return result;
            //Console.WriteLine("Tarefa Inserida " + result.Id);
        }

        [HttpGet]
        public async Task<TarefaModel> RetornaUltimoId()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetTaskAsync("Information/");
            var jsonResponse = JsonConvert.DeserializeObject<TarefaModel>(response.Body);
           // TarefaModel result = response.ResultAs<TarefaModel>();
            return null;
        }
    }
}