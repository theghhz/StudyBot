using Microsoft.AspNetCore.Mvc;
using StudyBot.Api.Results;
using StudyBot.Application.Dtos;
using StudyBot.Application.Services.ConteudoService;
using StudyBot.Application.Services.ConteudoService.Dtos;
using StudyBot.Infrastructure.Repositorios.ConteudoRepo;

namespace StudyBot.Api.Conteudo.Controller
{   // http://localhost:5269/swagger/index.html
    [Route("[controller]")]
    [ApiController]
    public class ConteudoController(IConteudoService conteudoService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType<ResultadoApi<ConteudoDto>>(StatusCodes.Status201Created)]
        [ProducesResponseType<ResultadoApi<EmptyResult>>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultadoApi<ConteudoDto>>> CriarConteudo(CriarConteudoDto dto)
        {
            var caseResult = await conteudoService.CriarConteudo(dto);
            
            if(caseResult.PossuiErros)
                return BadRequest(ResultadoApi<ConteudoDto>.CriarFracasso(caseResult.Erros!));
            
            return CreatedAtAction(nameof(CriarConteudo), ResultadoApi<ConteudoDto>.CriarSucesso(caseResult.Valor!));

        }
        
        [HttpGet]
        [ProducesResponseType<ResultadoApi<ConteudoDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultadoApi<EmptyResult>>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResultadoApi<ConteudoDto>>> BuscarTodosConteudos()
        {
            var caseResult = await conteudoService.BuscarTodosConteudos();
            
            return Ok(ResultadoApi<List<ConteudoDto>>.CriarSucesso(caseResult.Valor!));
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType<ResultadoApi<ConteudoDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultadoApi<EmptyResult>>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResultadoApi<ConteudoDto>>> BuscarConteudoPorId(Guid id)
        {
            var caseResult = await conteudoService.BuscarConteudoPorId(id);
            
            if(caseResult.PossuiErros)
                return NotFound(ResultadoApi<ConteudoDto>.CriarFracasso(caseResult.Erros!));
            
            return Ok(ResultadoApi<ConteudoDto>.CriarSucesso(caseResult.Valor!));
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType<ResultadoApi<ConteudoDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultadoApi<EmptyResult>>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultadoApi<ConteudoDto>>> AtualizarConteudo(Guid id, AtualizarConteudoDto dto)
        {
            var caseResult = await conteudoService.AtualizarConteudo(id, dto);
            
            if(caseResult.PossuiErros)
                return BadRequest(ResultadoApi<ConteudoDto>.CriarFracasso(caseResult.Erros!));
            
            return Ok(ResultadoApi<ConteudoDto>.CriarSucesso(caseResult.Valor!));
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType<ResultadoApi<string>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultadoApi<EmptyResult>>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResultadoApi<string>>> RemoverConteudo(Guid id)
        {
            var caseResult = await conteudoService.RemoverConteudo(id);
            
            if(caseResult.PossuiErros)
                return NotFound(ResultadoApi<string>.CriarFracasso(caseResult.Erros!));
            
            return Ok(ResultadoApi<string>.CriarSucesso(caseResult.Valor!));
        }
    }
}