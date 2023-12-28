namespace api.Context.Dtos.AfiliacaoDtos
{
    public class ReadAfiliacaoDto
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string NomeEmpresa { get; set; }
        public int AssociadoId { get; set; }
        public string NomeAssociado { get; set; }
    }
}
