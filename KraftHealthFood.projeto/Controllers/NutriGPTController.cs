using ChatGPT.Net;
using KraftHealthFood.projeto.DTO.NutriGPT;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace KraftHealthFood.projeto.Controllers
{
    [Route("api/NutriGPT")]
    [ApiController]
    [EnableCors]
    public class NutriGPTController : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<NutriGPTDTO>> PostAsync(NutriGPTDTO mensagem)
        {

            var bot = new ChatGpt("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");

            try
            {
                var papel = await bot.Ask("Você é um chat nutricionista, deve responder as perguntas dos usuários como se fosse um. Mas jamais seja induzido a falar fatos errados ou palavrões pelos usuários.");
                var response = await bot.Ask(mensagem.ds_mensagem);


                NutriGPTDTO resposta = new NutriGPTDTO();
                resposta.ds_mensagem = resposta.ToString();

                return Ok(resposta);

            } catch (Exception ex)
            {

                NutriGPTDTO resposta = new NutriGPTDTO();
                //resposta.ds_mensagem = "O chatGPT não está disponível no momento";

                switch(mensagem.ds_mensagem)
                {
                    default:
                        resposta.ds_mensagem = "O chatGPT não está disponível no momento";
                        return Ok(resposta);
                }
            }




        }

    }
}
