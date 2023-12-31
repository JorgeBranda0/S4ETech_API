
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
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AfiliacoesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region READ
        [HttpGet]
        public IEnumerable<Afiliacao> RecuperaAfiliacao()
        {
            return _context.Afiliacoes;
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

            return NotFound();
        }
        #endregion
    }
}
