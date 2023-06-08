using KraftHealthFood.projeto.DTO;
using KraftHealthFood.projeto.DTO.Usuario;
using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.Repository;
using KraftHealthFood.projeto.Repository.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace KraftHealthFood.projeto.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    [EnableCors]
    public class prod_cadastrarController : ControllerBase
    {

        private readonly khf_usuarioRepository _khf_usuarioRepository;

        public prod_cadastrarController(DataBaseContext context)
        {
            _khf_usuarioRepository = new khf_usuarioRepository(context);
        }



        [HttpPost("CadastrarUsuario/")]
        public ActionResult<khf_usuarioModel> Post([FromBody] UsuarioDTO novoUsuario)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            khf_usuarioModel usuario = new khf_usuarioModel();

            usuario.nm_usuario = novoUsuario.nm_usuario;
            usuario.nr_cpf = novoUsuario.nr_cpf;
            usuario.dt_nascimento = novoUsuario.dt_nascimento;
            usuario.ds_genero = novoUsuario.ds_genero;
            usuario.ds_telefone = novoUsuario.ds_telefone;
            usuario.endereco = novoUsuario.endereco;
            usuario.ds_email = novoUsuario.ds_email;
            usuario.ds_senha = novoUsuario?.ds_senha;

            var userTeste = _khf_usuarioRepository.ConsultarPorEmail(novoUsuario.ds_email.ToLower());
            if (userTeste != null) { 
                return BadRequest("Usuario já existe!");
            }

            _khf_usuarioRepository.Inserir(usuario);

            var location = new Uri(Request.GetEncodedUrl() + "/" + usuario.id_usuario);
            return Created(location, usuario);
        }


    }


    [Route("api/Usuario")]
    [ApiController]
    [EnableCors]
    public class prod_loginController : ControllerBase
    {

        private readonly khf_usuarioRepository _khf_usuarioRepository;

        public prod_loginController(DataBaseContext context)
        {
            _khf_usuarioRepository = new khf_usuarioRepository(context);
        }



        [HttpPost("LogarUsuario/")]
        public ActionResult<khf_usuarioModel> Post([FromBody] UsuarioLoginDTO usuarioLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = _khf_usuarioRepository.logar(usuarioLogin.ds_email, usuarioLogin.ds_senha);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

    }


    [Route("api/Usuario")]
    [ApiController]
    [EnableCors]
    public class prod_usuarioController : ControllerBase
    {

        private readonly khf_usuarioRepository _khf_usuarioRepository;
        private readonly khf_mensagemRepository _khf_mensagemRepository;

        public prod_usuarioController(DataBaseContext context)
        {
            _khf_usuarioRepository = new khf_usuarioRepository(context);
            _khf_mensagemRepository = new khf_mensagemRepository(context);
        }

        [HttpPut("AlterarUsuario/{id:int}")]
        public ActionResult<khf_usuarioModel> Put([FromRoute] int id, [FromBody] UsuarioAlterarDTO usuarioAlterado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (usuarioAlterado.id_usuario != id)
            {
                return NotFound();
            }

            var usuarioModel = _khf_usuarioRepository.Consultar(usuarioAlterado.id_usuario);
            if (usuarioModel == null)
            {
                return NotFound();
            }
            usuarioModel.nm_usuario = usuarioAlterado.nm_usuario;
            usuarioModel.nr_cpf = usuarioAlterado.nr_cpf;
            usuarioModel.dt_nascimento = usuarioAlterado.dt_nascimento;
            usuarioModel.ds_genero = usuarioAlterado.ds_genero;
            usuarioModel.ds_telefone = usuarioAlterado.ds_telefone;
            usuarioModel.ds_email = usuarioAlterado.ds_email;
            usuarioModel.ds_senha = usuarioAlterado.ds_senha;


            try
            {
                _khf_usuarioRepository.Alterar(usuarioModel);
                return NoContent();
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar os dados do Usuario. Detalhes: {error.Message}" });
            }
        }

        [HttpDelete("DeletarUsuario/{id:int}")]
        public ActionResult<JsonContent> Delete([FromRoute] int id)
        {

            try
            {
                try
                {
                    var mensagens = _khf_mensagemRepository.ConsultarPorIdUsuario(id);
                    _khf_mensagemRepository.ExcluirRange(mensagens);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

                var usuario = _khf_usuarioRepository.Consultar(id);
                _khf_usuarioRepository.Excluir(usuario);
                return Ok("Usuario excluido");


            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }


        [HttpPost("enviarMensagem")]
        public ActionResult<khf_mensagemModel> Post([FromBody] UsuarioMensagemDTO novaMensagem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            khf_mensagemModel mensagem = new khf_mensagemModel();

            mensagem.id_usuario = novaMensagem.id_usuario;
            mensagem.ds_conteudo_mensagem = novaMensagem.ds_conteudo_mensagem;
            mensagem.dt_mensagem = DateTime.Now;



            try
            {
                _khf_mensagemRepository.Inserir(mensagem);
            }
            catch (Exception e)
            {
                return BadRequest("Não foi possível enviar a mensagem");
            }

            khf_usuarioModel usuario = new khf_usuarioModel();
            usuario = _khf_usuarioRepository.Consultar(mensagem.id_usuario);

            UsuarioMensagemRetornoDTO mensagemRetorno = new UsuarioMensagemRetornoDTO();

            mensagemRetorno.id_usuario = novaMensagem.id_usuario;
            mensagemRetorno.ds_conteudo_mensagem = novaMensagem.ds_conteudo_mensagem;
            mensagemRetorno.nm_usuario = usuario.nm_usuario;
            mensagemRetorno.dt_mensagem = mensagem.dt_mensagem;



            return Ok(mensagemRetorno);
        }
    }


}
