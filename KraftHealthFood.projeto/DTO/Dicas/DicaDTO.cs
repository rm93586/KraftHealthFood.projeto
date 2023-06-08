using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KraftHealthFood.projeto.DTO.Dicas
{
    public class DicaDTO
    {
        [JsonIgnore]
        public int id_dica { get; set; }

        public string? ds_dica { get; set; }
    }
}
