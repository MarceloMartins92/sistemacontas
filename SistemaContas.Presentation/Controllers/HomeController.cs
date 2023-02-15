using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Enums;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentation.Models;

namespace SistemaContas.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new DashboardViewModel();

            try
            {
                var dataAtual = DateTime.Now;
                var dataIni = new DateTime(dataAtual.Year, dataAtual.Month, 1);
                var dataFim = dataIni.AddMonths(1).AddDays(-1);

                var contaRepository = new ContaRepository();
                var contas = contaRepository.GetByUsuarioAndDatas(UsuarioAutenticado.Id, dataIni, dataFim);

                var totalReceber = contas.Where(c => c.Tipo == TipoConta.Receber).Sum(c => c.Valor);
                var totalPagar = contas.Where(c => c.Tipo == TipoConta.Pagar).Sum(c => c.Valor);

                model.DataIni = dataIni.ToString("dd/MM/yyyy");
                model.DataFim = dataFim.ToString("dd/MM/yyyy");
                model.TotalPagar = totalPagar;
                model.TotalReceber = totalReceber;
                model.Saldo = totalReceber - totalPagar;
                model.Situacao = model.Saldo > 0 ? "Saldo positivo" : model.Saldo < 0 ? "Saldo negativo" : "Saldo nulo";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Falha ao gerar dashboard: " + e.Message;
            }

            return View(model); //enviando a model para a página
        }

        /// <summary>
        /// Método para retornar os dados do usuário autenticado
        /// </summary>
        private IdentityViewModel UsuarioAutenticado
        {
            get
            {
                var data = User.Identity.Name;
                return JsonConvert.DeserializeObject<IdentityViewModel>(data);
            }
        }
    }
}



