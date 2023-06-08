using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KraftHealthFood.projeto.Models
{
    [Table("KHF_DICA")]
    public class khf_dicaModel
    {
        [Key]
        [Column("ID_DICA")]
        public int id_dica { get; set; }

        [MaxLength(200)]
        [Column("DS_DICA")]
        public string? ds_dica { get; set; }

        public ICollection<khf_receitaModel>? receitas { get; set; }

        public khf_dicaModel()
        {
        }

        public khf_dicaModel(int id_dica)
        {
            id_dica = id_dica;
        }
    }
}
