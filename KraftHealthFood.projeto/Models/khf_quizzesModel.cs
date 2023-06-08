using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KraftHealthFood.projeto.Models
{
    [Table("KHF_QUIZZES")]
    public class khf_quizzesModel
    {
        [Key]
        [Column("ID_QUIZ")]
        public int id_quiz { get; set; }

        [MaxLength(200)]
        [Column("NM_QUIZ")]
        public string? nm_quiz { get; set; }

        [MaxLength(2000)]
        [Column("TX_QUIZ")]
        public string? tx_quiz { get; set; }

        [MaxLength(700)]
        [Column("DS_PERGUNTA")]
        public string? ds_pergunta { get; set; }

        [MaxLength(400)]
        [Column("DS_RESPOSTA_1")]
        public string? ds_resposta_1 { get; set; }

        [MaxLength(400)]
        [Column("DS_RESPOSTA_2")]
        public string? ds_resposta_2 { get; set; }

        [MaxLength(400)]
        [Column("DS_RESPOSTA_3")]
        public string? ds_resposta_3 { get; set; }


        [MaxLength(400)]
        [Column("DS_RESPOSTA_4")]
        public string? ds_resposta_4 { get; set; }

        [MaxLength(400)]
        [Column("DS_RESPOSTA_CERTA")]
        public string? ds_resposta_certa { get; set; }

        public khf_quizzesModel()
        {
        }

        public khf_quizzesModel(int id_quiz)
        {
            id_quiz = id_quiz;
        }
    }
}
