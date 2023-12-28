using System.ComponentModel.DataAnnotations;

namespace api.Context.Dtos.AfiliacaoDtos
{
    public class CreateAfiliacaoDto
    {
        public int EmpresaId { get; set; }
        public string NomeEmpresa { get; set; }
        public int AssociadoId { get; set; }
        public string NomeAssociado { get; set; }
    }
}
