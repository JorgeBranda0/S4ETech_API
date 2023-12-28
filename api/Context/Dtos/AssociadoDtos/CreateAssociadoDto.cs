using System.ComponentModel.DataAnnotations;

namespace api.Context.Dtos.AssociadoDtos
{
    public class CreateAssociadoDto
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo 'CPF' é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo 'Data de Nascimento' é obrigatório")]
        public DateTime DataNascimento { get; set; }
        public List<int> EmpresaId { get; set; }
    }
}