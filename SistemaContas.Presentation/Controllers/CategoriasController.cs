using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Entities;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentation.Models;
using SistemaContas.Reports.Services;

namespace SistemaContas.Presentation.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriasCadastroViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //preenchendo o objeto categoria
                    var categoria = new Categoria();
                    categoria.Id = Guid.NewGuid();
                    categoria.Nome = model.Nome;
                    categoria.IdUsuario = UsuarioAutenticado.Id;

                    //gravando no banco de dados
                    var categoriaRepository = new CategoriaRepository();
                    categoriaRepository.Add(categoria);

                    TempData["MensagemSucesso"] = $"Categoria {categoria.Nome}, cadastrada com sucesso.";
                    ModelState.Clear(); //limpar os campos do formulário
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = $"Falha ao cadastrar categoria: {e.Message}";
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário.";
            }

            return View();
        }

        public IActionResult Consulta()
        {
            var model = new List<CategoriasConsultaViewModel>();

            try
            {
                var categoriaRepository = new CategoriaRepository();
                foreach (var item in categoriaRepository.GetByUsuario(UsuarioAutenticado.Id))
                {
                    var categoriaConsultaViewModel = new CategoriasConsultaViewModel();
                    categoriaConsultaViewModel.Id = item.Id;
                    categoriaConsultaViewModel.Nome = item.Nome;

                    model.Add(categoriaConsultaViewModel);
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = "Falha ao consultar categorias: " + e.Message;
            }

            return View(model);
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                //capturar a categoria no banco de dados através do ID
                var categoriaRepository = new CategoriaRepository();
                var categoria = categoriaRepository.GetById(id);

                //verificando se a categoria foi encontrada e se
                //ela pertence ao usuário autenticado
                if(categoria != null && categoria.IdUsuario == UsuarioAutenticado.Id)
                {
                    //capturar a quantidade de contas da categoria selecionada
                    var qtdContas = categoriaRepository.CountContasByIdCategoria(id);

                    if(qtdContas == 0) //não há contas vinculadas para a categoria
                    {
                        //excluindo do banco de dados
                        categoriaRepository.Delete(categoria);

                        TempData["MensagemSucesso"] = "Categoria excluída com sucesso.";
                    }
                    else
                    {
                        TempData["MensagemAlerta"] = $"Não é possível excluir, pois existem {qtdContas} conta(s) cadastrada(s) para esta categoria.";
                    }                   
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = "Falha ao excluir categoria: " + e.Message;
            }

            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(Guid id)
        {
            var model = new CategoriasEdicaoViewModel();

            try
            {
                //consultar a categoria no banco de dados através do ID
                var categoriaRepository = new CategoriaRepository();
                var categoria = categoriaRepository.GetById(id);

                //verificar se a categoria foi encontrada e pertence ao usuário autenticado
                if(categoria != null && categoria.IdUsuario == UsuarioAutenticado.Id)
                {
                    //exibir os dados na página
                    model.Id = categoria.Id;
                    model.Nome = categoria.Nome;
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = "Falha ao obter categoria: " + e.Message;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(CategoriasEdicaoViewModel model)
        {
            //verificar se todos os campos da model passaram
            //nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    //buscando a categoria no banco de dados
                    var categoriaRepository = new CategoriaRepository();
                    var categoria = categoriaRepository.GetById(model.Id);

                    //verificando se a categoria existe e se pertence ao usuário autenticado
                    if(categoria != null && categoria.IdUsuario == UsuarioAutenticado.Id)
                    {
                        //atualizar os dados da categoria no banco de dados
                        categoria.Nome = model.Nome;
                        categoriaRepository.Update(categoria);

                        TempData["MensagemSucesso"] = "Categoria atualizada com sucesso!";
                        return RedirectToAction("Consulta");
                    }
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = "Falha ao atualizar categoria: " + e.Message;
                }
            }

            return View(model);
        }

        public IActionResult RelatorioExcel()
        {
            try
            {
                //consultar as categorias do usuário autenticado
                var categoriaRepository = new CategoriaRepository();
                var categorias = categoriaRepository.GetByUsuario(UsuarioAutenticado.Id);

                //gerar um relatório excel com as categorias
                var categoriasReportService = new CategoriasReportService();
                var relatorio = categoriasReportService.GerarRelatorioExcel(categorias);

                //Download do relatório
                return File(
                    relatorio,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                    "relatorio_categorias.xlsx");
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = "Falha ao gerar relatório: " + e.Message;
            }

            return RedirectToAction("Consulta");
        }

        public IActionResult RelatorioPdf()
        {
            try
            {
                //consultar as categorias do usuário autenticado
                var categoriaRepository = new CategoriaRepository();
                var categorias = categoriaRepository.GetByUsuario(UsuarioAutenticado.Id);

                //gerar um relatório pdf com as categorias
                var categoriasReportService = new CategoriasReportService();
                var relatorio = categoriasReportService.GerarRelatorioPdf(categorias);

                //Download do relatório
                return File(
                    relatorio,
                    "application/pdf",
                    "relatorio_categorias.pdf");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Falha ao gerar relatório: " + e.Message;
            }

            return RedirectToAction("Consulta");
        }

        /// <summary>
        /// Método para retornar os dados do usuário autenticado
        /// </summary>
        private IdentityViewModel UsuarioAutenticado
        {
            get {
                var data = User.Identity.Name;
                return JsonConvert.DeserializeObject<IdentityViewModel>(data);
            }
        }
    }
}
