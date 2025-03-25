using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Barbearia.API.Models
{
    public class Agendamento
    {
        [Key]
        public int AgendamentoId { get; set; }

        [Required(ErrorMessage = "O cliente � obrigat�rio.")]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "A data e hora do agendamento s�o obrigat�rias.")]
        [DataType(DataType.DateTime, ErrorMessage = "Formato de data e hora inv�lido.")]
        public DateTime DataHora { get; set; }

        [StringLength(500, ErrorMessage = "As observa��es devem ter no m�ximo 500 caracteres.")]
        public string? Observacoes { get; set; }

        [Required(ErrorMessage = "O status do agendamento � obrigat�rio.")]
        public StatusAgendamento Status { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        public virtual ICollection<Servico>? Servicos { get; set; }

    }

    // Enum para Status
    public enum StatusAgendamento
    {
        Pendente = 0,
        Confirmado = 1,
        Cancelado = 2
    }

}