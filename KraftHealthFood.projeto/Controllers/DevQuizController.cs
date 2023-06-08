using KraftHealthFood.projeto.DTO.Quizzes;
using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.Repository.Context;
using KraftHealthFood.projeto.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace KraftHealthFood.projeto.Controllers
{
    [Route("api/DEV/Quiz")]
    [ApiController]
    [EnableCors]
    public class DevQuizController : ControllerBase
    {
        private readonly khf_usuarioRepository _khf_usuarioRepository;
        private readonly khf_mensagemRepository _khf_mensagemRepository;
        private readonly khf_quizzesRepository _khf_quizzesRepository;
        private readonly khf_dicaRepository _khf_dicaRepository;
        private readonly khf_receitaRepository _khf_receitaRepository;


        public DevQuizController(DataBaseContext context)
        {
            _khf_usuarioRepository = new khf_usuarioRepository(context);
            _khf_mensagemRepository = new khf_mensagemRepository(context);
            _khf_quizzesRepository = new khf_quizzesRepository(context);
            _khf_dicaRepository = new khf_dicaRepository(context);
            _khf_receitaRepository = new khf_receitaRepository(context);
        }


        [HttpGet()]
        public ActionResult<List<khf_quizzesModel>> Get()
        {

            try
            {
                var lista = _khf_quizzesRepository.Listar();
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


        [HttpPost("AdicionarQuiz/")]
        public ActionResult<khf_quizzesModel> Post([FromBody] QuizzesDTO novoQuiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            khf_quizzesModel model = new khf_quizzesModel();

            model.nm_quiz = novoQuiz.nm_quiz;
            model.tx_quiz = novoQuiz.tx_quiz;
            model.ds_pergunta = novoQuiz.ds_pergunta;
            model.ds_resposta_1 = novoQuiz.ds_resposta_1;
            model.ds_resposta_2 = novoQuiz.ds_resposta_2;
            model.ds_resposta_3 = novoQuiz.ds_resposta_3;
            model.ds_resposta_4 = novoQuiz.ds_resposta_4;
            model.ds_resposta_certa = novoQuiz.ds_resposta_certa;


            _khf_quizzesRepository.Inserir(model);

            var location = new Uri(Request.GetEncodedUrl() + "/" + model.id_quiz);
            return Created(location, model);
        }

        [HttpDelete("DeletarQuiz/{id:int}")]
        public ActionResult<JsonContent> Delete([FromRoute] int id)
        {

            try
            {
                var quiz = _khf_quizzesRepository.Consultar(id);
                _khf_quizzesRepository.Excluir(quiz);
                return Ok("Quiz excluido");

            }
            catch (Exception e)
            {
                Console.WriteLine("Quiz não foi encontrado");
                return BadRequest("Quiz não foi encontrado");
            }

        }
    }
}
