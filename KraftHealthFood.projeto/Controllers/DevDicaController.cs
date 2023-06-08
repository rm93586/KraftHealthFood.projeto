using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.Repository.Context;
using KraftHealthFood.projeto.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using KraftHealthFood.projeto.DTO.Dicas;
using KraftHealthFood.projeto.DTO.Receitas;
using Microsoft.AspNetCore.Cors;

namespace KraftHealthFood.projeto.Controllers
{

    [Route("api/DEV/Dica")]
    [ApiController]
    [EnableCors]
    public class DevDicaController : ControllerBase
    {
        private readonly khf_usuarioRepository _khf_usuarioRepository;
        private readonly khf_mensagemRepository _khf_mensagemRepository;
        private readonly khf_quizzesRepository _khf_quizzesRepository;
        private readonly khf_dicaRepository _khf_dicaRepository;
        private readonly khf_receitaRepository _khf_receitaRepository;


        public DevDicaController(DataBaseContext context)
        {
            _khf_usuarioRepository = new khf_usuarioRepository(context);
            _khf_mensagemRepository = new khf_mensagemRepository(context);
            _khf_quizzesRepository = new khf_quizzesRepository(context);
            _khf_dicaRepository = new khf_dicaRepository(context);
            _khf_receitaRepository = new khf_receitaRepository(context);
        }

        [HttpGet()]
        public ActionResult<List<khf_dicaModel>> Get()
        {

            try
            {
                var lista = _khf_dicaRepository.Listar();
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


        [HttpPost("AdicionarDica/")]
        public ActionResult<khf_dicaModel> Post([FromBody] DicaDTO novaDica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            khf_dicaModel model = new khf_dicaModel();

            model.ds_dica = novaDica.ds_dica;


            _khf_dicaRepository.Inserir(model);

            var location = new Uri(Request.GetEncodedUrl() + "/" + model.id_dica);
            return Created(location, novaDica);
        }

        [HttpPut("AlterarDica/{id:int}")]
        public ActionResult<khf_dicaModel> Put([FromRoute] int id, [FromBody] DicaDTO dicaAlterada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dicaModel = _khf_dicaRepository.Consultar(id);
            

            if (dicaModel == null)
            {
                return NotFound();
            }

            dicaModel.ds_dica = dicaAlterada.ds_dica;

            try
            {
                _khf_dicaRepository.Alterar(dicaModel);
                return NoContent();
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar os dados do Criminoso. Detalhes: {error.Message}" });
            }
        }

        [HttpPut("AdicionarDicaReceita/")]
        public ActionResult<khf_dicaModel> Post([FromBody] DicaReceitaDTO receitaAdicionada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receitaModel = _khf_receitaRepository.Consultar(receitaAdicionada.id_receita);

            if (receitaModel == null)
            {
                return NotFound();
            }

            if (receitaModel.id_dica != null)
            {
                return BadRequest("Receita já possui dica");
            }

            receitaModel.id_dica = receitaAdicionada.id_dica;
            receitaModel.id_receita = receitaAdicionada.id_receita;

            _khf_receitaRepository.Alterar(receitaModel);

            var location = new Uri(Request.GetEncodedUrl() + "/" + receitaModel.id_dica);
            return Created(location, receitaAdicionada);
        }


        [HttpDelete("DeletarDica/{id:int}")]
        public ActionResult<JsonContent> Delete([FromRoute] int id)
        {

            try
            {
                var dica = _khf_dicaRepository.Consultar(id);
                _khf_dicaRepository.Excluir(dica);
                return Ok("Dica excluida");

            }
            catch (Exception e)
            {
                Console.WriteLine("Dica não foi encontrado");
                return BadRequest("Dica não foi encontrado");
            }

        }
    }
}
