using Microsoft.AspNetCore.Mvc;
using StudyBot.Api.Results;
using StudyBot.Application.Dtos;
using StudyBot.Application.Services.CronogramaService;
using StudyBot.Application.Services.CronogramaService.Dtos;

namespace StudyBot.Api.Cronograma.Controller
{   
    [Route("[controller]")]
    [ApiController]
    public class CronogramaController(ICronogramaService cronogramaService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType<ResultadoApi<CronogramaDto>>(StatusCodes.Status201Created)]
        [ProducesResponseType<ResultadoApi<EmptyResult>>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultadoApi<CronogramaDto>>> CriarCronograma(CriarCronogramaDto dto)
        {
            var caseResult = await cronogramaService.CriarCronograma(dto);
            
            if(caseResult.PossuiErros)
                return BadRequest(ResultadoApi<CronogramaDto>.CriarFracasso(caseResult.Erros!));
            
            return CreatedAtAction(nameof(CriarCronograma), ResultadoApi<CronogramaDto>.CriarSucesso(caseResult.Valor!));
        }
        
        [HttpGet]
        [ProducesResponseType<ResultadoApi<CronogramaDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultadoApi<EmptyResult>>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResultadoApi<CronogramaDto>>> BuscarTodosCronogramas()
        {
            var caseResult = await cronogramaService.BuscarTodosCronogramas();
            
            return Ok(ResultadoApi<List<CronogramaDto>>.CriarSucesso(caseResult.Valor!));
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType<ResultadoApi<CronogramaDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultadoApi<EmptyResult>>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResultadoApi<CronogramaDto>>> BuscarCronogramaPorId(Guid id)
        {
            var caseResult = await cronogramaService.BuscarCronogramaPorId(id);
            
            if(caseResult.PossuiErros)
                return NotFound(ResultadoApi<CronogramaDto>.CriarFracasso(caseResult.Erros!));
            
            return Ok(ResultadoApi<CronogramaDto>.CriarSucesso(caseResult.Valor!));
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType<ResultadoApi<CronogramaDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultadoApi<EmptyResult>>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultadoApi<CronogramaDto>>> AtualizarCronograma(Guid id, [FromBody]EditarCronogramaDto dto)
        {
            var caseResult = await cronogramaService.EditarCronograma(id, dto);
            
            if(caseResult.PossuiErros)
                return BadRequest(ResultadoApi<CronogramaDto>.CriarFracasso(caseResult.Erros!));
            
            return Ok(ResultadoApi<CronogramaDto>.CriarSucesso(caseResult.Valor!));
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType<ResultadoApi<CronogramaDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResultadoApi<EmptyResult>>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResultadoApi<string>>> DeletarCronograma(Guid id)
        {
            var caseResult = await cronogramaService.DeletarCronograma(id);
            
            if(caseResult.PossuiErros)
                return NotFound(ResultadoApi<string>.CriarFracasso(caseResult.Erros!));
            
            return Ok(ResultadoApi<string>.CriarSucesso(caseResult.Valor!));
        }
    }
}