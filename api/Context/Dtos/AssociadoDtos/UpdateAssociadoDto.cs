using System.ComponentModel.DataAnnotations;

namespace api.Context.Dtos.AssociadoDtos
{
    public class UpdateAssociadoDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
