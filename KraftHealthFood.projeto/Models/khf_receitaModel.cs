using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KraftHealthFood.projeto.Models
{
    [Table("KHF_RECEITAS")]
    public class khf_receitaModel
    {
        [Key]
        [Column("ID_RECEITA")]
        public int id_receita { get; set; }

        
        [Column("ID_DICA")]
        public int? id_dica { get; set; }

        [JsonIgnore]
        public khf_dicaModel? dica { get; set; }

        [MaxLength(200)]
        [Column("NM_RECEITA")]
        public string? nm_receita { get; set; }

        [MaxLength(200)]
        [Column("DS_RECEITA")]
        public string? ds_receita { get; set; }

        [MaxLength(300)]
        [Column("DS_IMAGEM")]
        public string? ds_imagem { get; set; }

        [MaxLength(300)]
        [Column("DS_LINK_RECEITA")]
        public string? ds_link_receita { get; set; }

        public khf_receitaModel()
        {
        }

        public khf_receitaModel(int id_receita)
        {
            id_receita = id_receita;
        }
    }
}
