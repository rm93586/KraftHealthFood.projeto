using KraftHealthFood.projeto.Models;
using KraftHealthFood.projeto.Repository.Context;
using KraftHealthFood.projeto.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using KraftHealthFood.projeto.DTO.Receitas;
using KraftHealthFood.projeto.DTO.Dicas;
using Microsoft.AspNetCore.Cors;

namespace KraftHealthFood.projeto.Controllers
{
    [Route("api/DEV/Receita")]
    [ApiController]
    [EnableCors]
    public class DevReceitaController : ControllerBase
    {
        private readonly khf_usuarioRepository _khf_usuarioRepository;
        private readonly khf_mensagemRepository _khf_mensagemRepository;
        private readonly khf_quizzesRepository _khf_quizzesRepository;
        private readonly khf_dicaRepository _khf_dicaRepository;
        private readonly khf_receitaRepository _khf_receitaRepository;


        public DevReceitaController(DataBaseContext context)
        {
            _khf_usuarioRepository = new khf_usuarioRepository(context);
            _khf_mensagemRepository = new khf_mensagemRepository(context);
            _khf_quizzesRepository = new khf_quizzesRepository(context);
            _khf_dicaRepository = new khf_dicaRepository(context);
            _khf_receitaRepository = new khf_receitaRepository(context);
        }

        [HttpGet()]
        public ActionResult<List<khf_receitaModel>> Get()
        {

            try
            {
                var lista = _khf_receitaRepository.Listar();
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

        [HttpPost("AdicionarReceita/")]
        public ActionResult<khf_receitaModel> Post([FromBody] ReceitaDTO novaReceita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            khf_receitaModel receitaModel = new khf_receitaModel();

            receitaModel.nm_receita = novaReceita.nm_receita;
            receitaModel.ds_receita = novaReceita.ds_receita;
            receitaModel.ds_imagem = novaReceita.ds_imagem;
            receitaModel.ds_link_receita = novaReceita.ds_link_receita;

            _khf_receitaRepository.Inserir(receitaModel);

            var location = new Uri(Request.GetEncodedUrl() + "/" + receitaModel.id_receita);
            return Created(location, receitaModel);
        }

        [HttpPut("AlterarReceita/{id:int}")]
        public ActionResult<khf_usuarioModel> Put([FromRoute] int id, [FromBody] ReceitaDTO receitaAlterada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receitaModel = _khf_receitaRepository.Consultar(id);
            if (receitaModel == null)
            {
                return NotFound();
            }

            receitaModel.nm_receita = receitaAlterada.nm_receita;
            receitaModel.ds_receita = receitaAlterada.ds_receita;
            receitaModel.ds_imagem = receitaAlterada.ds_imagem;
            receitaModel.ds_link_receita = receitaModel.ds_link_receita;

            try
            {
                _khf_receitaRepository.Alterar(receitaModel);
                return NoContent();
            }
            catch (Exception error)
            {
                return BadRequest(new { message = $"Não foi possível alterar os dados do Criminoso. Detalhes: {error.Message}" });
            }
        }

        [HttpPut("AdicionarReceitaDica/")]
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
                return BadRequest("Receita já possui Dica");
            }

            var dicaModel = _khf_dicaRepository.Consultar(receitaAdicionada.id_dica);
            if (dicaModel == null)
            {
                return NotFound();
            }



            receitaModel.id_dica = receitaAdicionada.id_dica;
            receitaModel.id_receita = receitaAdicionada.id_receita;

            _khf_receitaRepository.Alterar(receitaModel);

            var location = new Uri(Request.GetEncodedUrl() + "/" + receitaModel.id_dica);
            return Created(location, receitaAdicionada);
        }

        [HttpDelete("DeletarReceita/{id:int}")]
        public ActionResult<JsonContent> Delete([FromRoute] int id)
        {

            try
            {
                var receita = _khf_receitaRepository.Consultar(id);
                _khf_receitaRepository.Excluir(receita);
                return Ok("Receita excluida");

            }
            catch (Exception e)
            {
                Console.WriteLine("Receita não foi encontrado");
                return BadRequest("Receita não foi encontrado");
            }

        }
    }
}
