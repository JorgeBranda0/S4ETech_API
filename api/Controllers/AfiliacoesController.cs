
using api.Context;
using api.Context.Dtos.AfiliacaoDtos;
using api.Context.Dtos.AssociadoDtos;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AfiliacoesController : ControllerBase
    {
        #region Construtores
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AfiliacoesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region READ
        [HttpGet]
        public IActionResult RecuperaAfiliacao()
        {
            var afiliacoes = _context.Afiliacoes;
            
            if (afiliacoes.Any()) 
                return Ok(afiliacoes);
            else
                return NotFound("Não existem afiliações para exibir no momento.");
        }

        [HttpGet("{afiliacaoId}")]
        public IActionResult RecuperaAfiliacaoPorId(int afiliacaoId)
        {
            Afiliacao afiliacao = _context.Afiliacoes.FirstOrDefault(p => p.Id == afiliacaoId);
            
            if (afiliacao != null)
            {
                ReadAfiliacaoDto afiliacaoDto = _mapper.Map<ReadAfiliacaoDto>(afiliacao);
                return Ok(afiliacao);
            }

            return NotFound($"Não foi possível encontrar a afiliação mencionada: {afiliacaoId}");
        }
        #endregion
    }
}
