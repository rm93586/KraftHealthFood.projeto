using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KraftHealthFood.projeto.Models
{
    [Table("KHF_MENSAGENS")]
    public class khf_mensagemModel
    {

        [Column("ID_USUARIO")]
        public int id_usuario { get; set; }

        [Key]
        [Column("ID_MENSAGEM")]
        public int id_mensagem { get; set; }

        public khf_usuarioModel? usuario { get; set; }

        [MaxLength(1000)]
        [Column("DS_CONTEUDO_MENSAGEM")]
        public string? ds_conteudo_mensagem { get; set; }

        [Column("DT_MENSAGEM")]
        public DateTime? dt_mensagem { get; set; }


        public khf_mensagemModel()
        {
        }

        public khf_mensagemModel(int id_usuario, int id_mensagem)
        {
            id_usuario = id_usuario;
            id_mensagem = id_mensagem;
        }

    }
}
