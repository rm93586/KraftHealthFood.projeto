using KraftHealthFood.projeto.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KraftHealthFood.projeto.DTO.Mensagem
{
    public class MensagemAnonimaDTO
    {

        public string? ds_conteudo_mensagem { get; set; }

        public DateTime? dt_mensagem { get; set; }
    }
}
