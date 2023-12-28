namespace api.Context.Dtos.EmpresaDtos
{
    public class ReadEmpresaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public List<int> AssociadoId { get; set; }
    }
}
