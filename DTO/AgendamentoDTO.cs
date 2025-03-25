using Barbearia.API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Barbearia.API.DTO
{
    public class AgendamentoDTO
    {
        public int AgendamentoId { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório.")]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "A data e hora do agendamento são obrigatórias.")]
        [DataType(DataType.DateTime, ErrorMessage = "Formato de data e hora inválido.")]
        public DateTime DataHora { get; set; }

        [StringLength(500, ErrorMessage = "As observações devem ter no máximo 500 caracteres.")]
        public string? Observacoes { get; set; }

        [Required(ErrorMessage = "O status do agendamento é obrigatório.")]
        public StatusAgendamento Status { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        public virtual ICollection<Servico>? Servicos { get; set; }

    }
}
