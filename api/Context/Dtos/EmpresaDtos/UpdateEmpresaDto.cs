using System.ComponentModel.DataAnnotations;

namespace api.Context.Dtos.EmpresaDtos
{
    public class UpdateEmpresaDto
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
    }
}
