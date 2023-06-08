using KraftHealthFood.projeto.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KraftHealthFood.projeto.DTO.Usuario
{
    public class UsuarioMensagemDTO
    {

        public int id_usuario { get; set; }

        public string? ds_conteudo_mensagem { get; set; }

    }
}
