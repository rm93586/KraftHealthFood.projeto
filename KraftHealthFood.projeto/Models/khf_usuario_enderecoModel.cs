using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KraftHealthFood.projeto.Models
{
    [Table("KHF_USUARIO_ENDERECO")]
    public class khf_usuario_enderecoModel
    {
        [JsonIgnore]
        public khf_usuarioModel? cliente { get; set; }

        [JsonIgnore]
        [Column("ID_USUARIO")]
        public int id_usuario { get; set; }

        [MaxLength(200)]
        [Column("NM_RUA")]
        public string? nm_rua { get; set; }

        [MaxLength(9)]
        [Column("NR_CEP")]
        public string? nr_cep { get; set; }

        [MaxLength(200)]
        [Column("NM_BAIRRO")]
        public string? nm_bairro { get; set; }

        [MaxLength(200)]
        [Column("NM_CIDADE")]
        public string? nm_cidade { get; set; }

        [MaxLength(200)]
        [Column("NM_PAIS")]
        public string? nm_pais { get; set; }


        public khf_usuario_enderecoModel()
        {
        }

        public khf_usuario_enderecoModel(int id_usuario)
        {
            id_usuario = id_usuario;
        }
    }
}
