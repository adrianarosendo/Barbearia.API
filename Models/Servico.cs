using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbearia.API.Models
{
    public class Servico{ 
    
    [Key]
    public int Id { get; set; }

    [DisplayName("Nome do Serviço")]
    [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do serviço deve ter no máximo 100 caracteres.")]
    public string? NomeServico { get; set; }

    [DisplayName("Duração em minutos")]
    [Required(ErrorMessage = "A duração é obrigatória.")]
    [Range(1, 999, ErrorMessage = "A duração deve estar entre 1 e 999 minutos.")]
    public int DuracaoMin { get; set; }

    [DisplayName("Preço")]
    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, 999999.99, ErrorMessage = "O preço deve ser maior que 0.")]
    [Column(TypeName = "decimal(10,2)")]
    public double Preco { get; set; }
    }
}