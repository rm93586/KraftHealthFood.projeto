using KraftHealthFood.projeto.Repository.Context;
using KraftHealthFood.projeto.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.DTO.Usuario;
using KraftHealthFood.projeto.DTO.Mensagem;
using Microsoft.AspNetCore.Cors;

namespace KraftHealthFood.projeto.Controllers
{
    [Route("api/DEV/Mensagem")]
    [ApiController]
    [EnableCors]
    public class DevMensagemController : ControllerBase
    {
        private readonly khf_usuarioRepository _khf_usuarioRepository;
        private readonly khf_mensagemRepository _khf_mensagemRepository;
        private readonly khf_quizzesRepository _khf_quizzesRepository;
        private readonly khf_dicaRepository _khf_dicaRepository;
        private readonly khf_receitaRepository _khf_receitaRepository;


        public DevMensagemController(DataBaseContext context)
        {
            _khf_usuarioRepository = new khf_usuarioRepository(context);
            _khf_mensagemRepository = new khf_mensagemRepository(context);
            _khf_quizzesRepository = new khf_quizzesRepository(context);
            _khf_dicaRepository = new khf_dicaRepository(context);
            _khf_receitaRepository = new khf_receitaRepository(context);
        }

        [HttpGet()]
        public ActionResult<List<khf_mensagemModel>> Get()
        {

            try
            {
                var lista = _khf_mensagemRepository.ListarRecentes();

                if (lista != null)
                {
                    return Ok(lista);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e) { return StatusCode(StatusCodes.Status500InternalServerError); }

        }

        [HttpPost("enviarAnonimo")]
        public ActionResult<khf_mensagemModel> Post([FromBody] MensagemAnonimaDTO novaMensagem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            khf_mensagemModel mensagem = new khf_mensagemModel();

            var usuarioModel = _khf_usuarioRepository.ConsultarPorEmail("ANONIMO@ANONIMO.COM");

            if (usuarioModel == null)
            {
                usuarioModel = new khf_usuarioModel();
                usuarioModel.nm_usuario = "ANONIMO";
                usuarioModel.nr_cpf = "000.000.000-00";
                usuarioModel.dt_nascimento = DateTime.Now;
                usuarioModel.ds_genero = "OUTRO";
                usuarioModel.ds_telefone = "00 000000000";
                usuarioModel.ds_email = "ANONIMO@ANONIMO.COM";
                usuarioModel.ds_senha = "ASDNASJAFLASKASDMASNASLAJFSLKAFKLNASLFASKFHJAOIH";
                var endereco = new khf_usuario_enderecoModel();
                endereco.nr_cep = "00000000";
                endereco.nm_rua = "sem rua";
                endereco.nm_bairro = "sem bairro";
                endereco.nm_cidade = "sem cidade";
                endereco.nm_pais = "sem pais";
                _khf_usuarioRepository.Inserir(usuarioModel);
            }


            
            mensagem.id_usuario = usuarioModel.id_usuario;
            mensagem.ds_conteudo_mensagem = novaMensagem.ds_conteudo_mensagem;
            mensagem.dt_mensagem = DateTime.Now;



            _khf_mensagemRepository.Inserir(mensagem);


            return Ok(mensagem);
        }

        [HttpDelete("DeletarMensagem/{id:int}")]
        public ActionResult<JsonContent> Delete([FromRoute] int id)
        {
            try
            {
                var mensagem = _khf_mensagemRepository.ConsultarPoridMensagem(id);
                _khf_mensagemRepository.Excluir(mensagem);
                return Ok("Mensagem excluida");
            }
            catch (Exception e)
            {
                Console.WriteLine("Mensagem não foi encontrado");
                return BadRequest("Mensagem não foi encontrado");
            }

        }
    }
}
