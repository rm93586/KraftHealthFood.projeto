using KraftHealthFood.projeto.Repository.Context;
using KraftHealthFood.projeto.Repository;
using Microsoft.AspNetCore.Mvc;
using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.DTO;
using Microsoft.AspNetCore.Http.Extensions;
using KraftHealthFood.projeto.DTO.Avisos;
using KraftHealthFood.projeto.DTO.Quizzes;
using Microsoft.AspNetCore.Cors;

namespace KraftHealthFood.projeto.Controllers
{
    [Route("api/DEV/Usuario")]
    [ApiController]
    [EnableCors]
    public class DevUsuarioController : ControllerBase
    {
        private readonly khf_usuarioRepository _khf_usuarioRepository;
        private readonly khf_mensagemRepository _khf_mensagemRepository;
        private readonly khf_quizzesRepository _khf_quizzesRepository;
        private readonly khf_dicaRepository _khf_dicaRepository;
        private readonly khf_receitaRepository _khf_receitaRepository;


        public DevUsuarioController(DataBaseContext context)
        {
            _khf_usuarioRepository = new khf_usuarioRepository(context);
            _khf_mensagemRepository = new khf_mensagemRepository(context);
            _khf_quizzesRepository = new khf_quizzesRepository(context);
            _khf_dicaRepository = new khf_dicaRepository(context);
            _khf_receitaRepository = new khf_receitaRepository(context);
        }

        [HttpGet()]
        public ActionResult<JsonContent> Get()
        {

            try
            {
                var lista = _khf_usuarioRepository.Listar();

                if (lista != null)
                {
                    return Ok(lista);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                } catch (Exception e)
                {
                    return BadRequest(e.Message);
                }


                var usuario = _khf_usuarioRepository.Consultar(id);
                _khf_usuarioRepository.Excluir(usuario);
                return Ok("Usuario excluido");

            }
            catch (Exception e)
            {
                Console.WriteLine("Usuario não foi encontrado");
                return BadRequest("Usuario não foi encontrado ou não foi possível deletar");
            }

        }
    }
}