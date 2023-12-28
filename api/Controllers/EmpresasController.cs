using api.Context;
using api.Context.Dtos.AfiliacaoDtos;
using api.Context.Dtos.AssociadoDtos;
using api.Context.Dtos.EmpresaDtos;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmpresasController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CREATE
        [HttpPost]
        public async Task<IActionResult> CadastraEmpresas([FromBody] List<CreateEmpresaDto> empresaDto)
        {
            List<Empresa> empresas = _mapper.Map<List<Empresa>>(empresaDto);
            CreateAfiliacaoDto afiliacaoDto = new();
            List<Afiliacao> afiliacoes = new();

            try
            {
                foreach (var infoEmpresa in empresas)
                {
                    if (infoEmpresa.AssociadosId != null)
                    {
                        foreach (var associadoId in infoEmpresa.AssociadosId)
                        {
                            var associado = _context.Associados.FirstOrDefault(p => p.Id == associadoId);
                            afiliacaoDto = new()
                            {
                                EmpresaId = infoEmpresa.Id,
                                NomeEmpresa = infoEmpresa.Nome,
                                AssociadoId = associado.Id,
                                NomeAssociado = associado.Nome
                            };
                        }

                        afiliacoes = _mapper.Map<List<Afiliacao>>(afiliacaoDto);
                        _context.Afiliacoes.AddRange(afiliacoes);
                    }
                }

                _context.Empresas.AddRange(empresas);
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
        public IEnumerable<Empresa> RecuperaEmpresa()
        {
            return _context.Empresas;
        }

        [HttpGet("{empresaId}")]
        public IActionResult RecuperaEmpresaPorId(int empresaId)
        {
            Empresa empresa = _context.Empresas.FirstOrDefault(p => p.Id == empresaId);
            if (empresa != null)
            {
                ReadEmpresaDto empresaDto = _mapper.Map<ReadEmpresaDto>(empresa);
                return Ok(empresa);
            }

            return NotFound();
        }
        #endregion

        #region UPDATE
        [HttpPut("{empresaId}")]
        public async Task<IActionResult> AtualizaEmpresa(int empresaId,
                                                         int afiliarNovoAssociadoPorId,
                                                         int excluirAssociadoPorId,
                                                         [FromBody] UpdateEmpresaDto empresaDto) 
        {
            CreateAfiliacaoDto afiliacaoDto = new();
            Afiliacao afiliacao = new();

            List<Empresa> empresa = _context.Empresas.Where(p => p.Id == empresaId).ToList();
            if (empresa != null) 
            {
                foreach (var item in empresa) 
                {
                    if (empresaDto.Nome != "string" || string.IsNullOrEmpty(empresaDto.Nome) ||
                        empresaDto.Cnpj != "string" || string.IsNullOrEmpty(empresaDto.Cnpj))
                    {
                        var atualizaNomeEmpresa = _context.Afiliacoes.Where(p => p.EmpresaId == item.Id).ToList();
                        foreach (var atualizaEmpresa in atualizaNomeEmpresa) 
                        {
                            atualizaEmpresa.NomeEmpresa = empresaDto.Nome;
                            _context.Afiliacoes.Update(atualizaEmpresa);
                        }

                        _mapper.Map(empresaDto, item);
                        _context.Empresas.Update(item);
                    }

                    if (afiliarNovoAssociadoPorId > 0) 
                    {
                        var associado = _context.Associados.FirstOrDefault(p => p.Id == afiliarNovoAssociadoPorId);
                        if (associado != null) 
                        {
                            afiliacaoDto = new()
                            {
                                EmpresaId = item.Id,
                                NomeEmpresa = item.Nome,
                                AssociadoId = associado.Id,
                                NomeAssociado = associado.Nome
                            };

                            afiliacao = _mapper.Map<Afiliacao>(afiliacaoDto);
                            _context.Afiliacoes.Add(afiliacao);
                        }
                    }

                    if (excluirAssociadoPorId > 0) 
                    {
                        var associadoId = _context.Associados.FirstOrDefault(p => p.Id == excluirAssociadoPorId).Id;
                        if (associadoId > 0) 
                        {
                            var afiliacoes = _context.Afiliacoes.Where(p => p.AssociadoId == associadoId)
                                                                .Where(p => p.EmpresaId == item.Id)
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
        [HttpDelete("{empresaId}")]
        public IActionResult ExcluiEmpresa(int empresaId)
        {
            Empresa empresa = _context.Empresas.FirstOrDefault(p => p.Id == empresaId);
            if (empresa != null)
            {
                List<Afiliacao> afiliacoes = _context.Afiliacoes.Where(p => p.EmpresaId == empresa.Id).ToList();
                foreach (var item in afiliacoes)
                {
                    _context.Afiliacoes.Remove(item);
                }
                _context.Empresas.Remove(empresa);

                _context.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }
        #endregion

    }
}
