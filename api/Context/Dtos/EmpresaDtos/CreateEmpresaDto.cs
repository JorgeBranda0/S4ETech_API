using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Context.Dtos.EmpresaDtos
{
    public class CreateEmpresaDto
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo 'Cnpj' é obrigatório")]
        public string Cnpj { get; set; }
        public List<int> AssociadosId { get; set; }
    }
}
