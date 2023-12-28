using api.Context;
using api.Context.Dtos.AfiliacaoDtos;
using api.Context.Dtos.AssociadoDtos;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssociadosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AssociadosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CREATE
        [HttpPost]
        public async Task<IActionResult> CadastraAssociados([FromBody] List<CreateAssociadoDto> associadoDto)
        {
            List<Associado> associados = _mapper.Map<List<Associado>>(associadoDto);
            CreateAfiliacaoDto afiliacaoDto = new();
            List<Afiliacao> afiliacoes = new();

            try
            {
                foreach (var infoAssociado in associados)
                {
                    if (infoAssociado.EmpresasId != null)
                    {
                        foreach (var empresaId in infoAssociado.EmpresasId)
                        {
                            var empresa = _context.Empresas.FirstOrDefault(p => p.Id == empresaId);
                            afiliacaoDto = new()
                            {
                                EmpresaId = empresa.Id,
                                NomeEmpresa = empresa.Nome,
                                AssociadoId = infoAssociado.Id,
                                NomeAssociado = infoAssociado.Nome
                            };
                        }

                        afiliacoes = _mapper.Map<List<Afiliacao>>(afiliacaoDto);
                        _context.Afiliacoes.AddRange(afiliacoes);
                    }
                }

                _context.Associados.AddRange(associados);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region READ
        [HttpGet]
        public IEnumerable<Associado> RecuperaAssociados()
        {
            return _context.Associados;
        }

        [HttpGet("{associadoId}")]
        public IActionResult RecuperaAssociadosPorId(int associadoId)
        {
            Associado associado = _context.Associados.FirstOrDefault(p => p.Id == associadoId);
            if (associado != null)
            {
                ReadAssociadoDto associadoDto = _mapper.Map<ReadAssociadoDto>(associado);
                return Ok(associado);
            }

            return NotFound();
        }
        #endregion

        #region UPDATE
        [HttpPut("{associadoId}")]
        public async Task<IActionResult> AtualizaAssociado(int associadoId,
                                                           int afiliarNovaEmpresaPorId,
                                                           int excluirEmpresaPorId,
                                                           [FromBody] UpdateAssociadoDto associadoDto)
        {
            CreateAfiliacaoDto afiliacaoDto = new();
            Afiliacao afiliacao = new();

            List<Associado> associado = _context.Associados.Where(p => p.Id == associadoId).ToList();
            if (associado != null)
            {
                foreach (var item in associado)
                {
                    if (associadoDto.Nome != "string" || string.IsNullOrEmpty(associadoDto.Nome) || 
                        associadoDto.Cpf != "string" || string.IsNullOrEmpty(associadoDto.Cpf)) 
                    {
                        var atualizaNomeAssociado = _context.Afiliacoes.Where(p => p.AssociadoId == item.Id).ToList();
                        foreach (var atualizaAssociado in atualizaNomeAssociado) 
                        {
                            atualizaAssociado.NomeAssociado = associadoDto.Nome;
                            _context.Afiliacoes.Update(atualizaAssociado);
                        }

                        _mapper.Map(associadoDto, item);
                        _context.Associados.Update(item);
                    }

                    if (afiliarNovaEmpresaPorId > 0)
                    {
                        var empresa = _context.Empresas.FirstOrDefault(p => p.Id == afiliarNovaEmpresaPorId);
                        if (empresa != null)
                        {
                            afiliacaoDto = new()
                            {
                                EmpresaId = empresa.Id,
                                NomeEmpresa = empresa.Nome,
                                AssociadoId = item.Id,
                                NomeAssociado = item.Nome
                            };

                            afiliacao = _mapper.Map<Afiliacao>(afiliacaoDto);
                            _context.Afiliacoes.Add(afiliacao);
                        }
                    }

                    if (excluirEmpresaPorId > 0)
                    {
                        var empresaId = _context.Empresas.FirstOrDefault(p => p.Id == excluirEmpresaPorId).Id;
                        if (empresaId > 0)
                        {
                            var afiliacoes = _context.Afiliacoes.Where(p => p.EmpresaId == empresaId)
                                                                .Where(p => p.AssociadoId == item.Id)
                                                                .FirstOrDefault();

                            _context.Afiliacoes.Remove(afiliacoes);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return NoContent();
            }

            return NotFound();
        }
        #endregion

        #region DELETE
        [HttpDelete("{associadoId}")]
        public IActionResult ExcluiAssociado(int associadoId)
        {
            Associado associado = _context.Associados.FirstOrDefault(p => p.Id == associadoId);
            if (associado != null)
            {
                List<Afiliacao> afiliacoes = _context.Afiliacoes.Where(p => p.AssociadoId == associado.Id).ToList();
                foreach (var item in afiliacoes)
                {
                    _context.Afiliacoes.Remove(item);
                }
                _context.Associados.Remove(associado);

                _context.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }
        #endregion
    }
}
