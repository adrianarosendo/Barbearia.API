using System.ComponentModel.DataAnnotations;

namespace Barbearia.API.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O nome � obrigat�rio.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no m�ximo 100 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O telefone � obrigat�rio.")]
        [Phone(ErrorMessage = "O telefone informado n�o � v�lido.")]
        [StringLength(15, ErrorMessage = "O telefone deve ter no m�ximo 15 caracteres.")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O e-mail � obrigat�rio.")]
        [EmailAddress(ErrorMessage = "O e-mail informado n�o � v�lido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento � obrigat�ria.")]
        [DataType(DataType.Date, ErrorMessage = "Data de nascimento inv�lida.")]
        public DateTime DataNascimento { get; set; }
    }

}