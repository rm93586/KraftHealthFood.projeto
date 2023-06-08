using System.Text.Json.Serialization;
namespace KraftHealthFood.projeto.DTO.Avisos
{
    public class MensagemErroDTO
    {
        public string? mensagem { get; set; }
        public DateTime? data_mensagem { get; set; }

        [JsonIgnore]
        public string? codigo_erro { get; set; }
    }
}
