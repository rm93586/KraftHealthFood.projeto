using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KraftHealthFood.projeto.Models
{
    [Table("KHF_USUARIOS")]
    public class khf_usuarioModel
    {

        [Key]
        [Column("ID_USUARIO")]
        public int id_usuario { get; set; }

        [MaxLength(150)]
        [Column("NM_USUARIO")]
        public string? nm_usuario { get; set; }

        [MaxLength(14)]
        [Column("NR_CPF")]
        public string? nr_cpf { get; set; }

        [Column("DT_NASCIMENTO")]
        public DateTime? dt_nascimento { get; set; }

        [MaxLength(10)]
        [Column("DS_GENERO")]
        public string? ds_genero { get; set; }


        [MaxLength(14)]
        [Column("DS_TELEFONE")]
        public string? ds_telefone { get; set; }

        public khf_usuario_enderecoModel? endereco { get; set; }

        [MaxLength(150)]
        [Column("DS_EMAIL")]
        public string? ds_email { get; set; }

        [MaxLength(50)]
        [Column("DS_SENHA")]
        [JsonIgnore]
        public string? ds_senha { get; set; }

        [JsonIgnore]
        public ICollection<khf_mensagemModel>? mensagens { get; set; }




        public khf_usuarioModel()
        {
        }

        public khf_usuarioModel(int id_usuario)
        {
            id_usuario = id_usuario;
        }
    }
}
