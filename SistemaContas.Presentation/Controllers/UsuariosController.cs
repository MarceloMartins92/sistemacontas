using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Helpers;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentation.Models;

namespace SistemaContas.Presentation.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        public IActionResult MinhaConta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MinhaConta(AlterarSenhaViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //capturando os dados do usuário autenticado (Cookie de autenticação)
                    var data = User.Identity.Name;
                    var identityViewModel = JsonConvert.DeserializeObject<IdentityViewModel>(data);

                    //atualizando a senha do usuário no banco de dados
                    var usuarioRepository = new UsuarioRepository();
                    usuarioRepository.Update(identityViewModel.Id, MD5Helper.Encrypt(model.NovaSenha));

                    TempData["MensagemSucesso"] = "Senha de acesso atualizada com sucesso.";
                    ModelState.Clear();
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = "Falha ao atualizar a senha do usuário: " + e.Message;
                }
            }

            return View();
        }
    }
}
