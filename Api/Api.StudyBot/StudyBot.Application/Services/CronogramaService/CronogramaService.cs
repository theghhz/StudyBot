using StudyBot.Application.Dtos;
using StudyBot.Application.Services.CronogramaService.Dtos;
using StudyBot.Domain.Conteudos;
using StudyBot.Domain.Cronogramas;
using StudyBot.Domain.DiasSemanas;
using StudyBot.Domain.DiasSemanas.Enum;
using StudyBot.Domain.Erros;
using StudyBot.Infrastructure.Repositorios.ConteudoRepo;
using StudyBot.Infrastructure.Repositorios.CronogramaRepo;

namespace StudyBot.Application.Services.CronogramaService;

public class CronogramaService(ICronogramaRepositorio cronogramaRepositorio,
    IConteudoRepositorio conteudoRepositorio) : ICronogramaService
{
    
    public async Task<Resultado<CronogramaDto>> CriarCronograma(CriarCronogramaDto input)
    {
        var erros = new List<ErroEntidade>();
    
        var diasSemanaConvertidos = new List<DiasSemana>();

        foreach (DiasEnum diaEnum in Enum.GetValues(typeof(DiasEnum)))
        {
            var diaCriado = new DiasSemanaBuilder()
                .ComDia(diaEnum)
                .ComData(CalcularDataParaProximoDia(diaEnum))
                .ComConteudos(new List<Conteudo>())
                .Builder();
                
                //DiasSemana.Criar(diaEnum, DateTime.Now, new List<Conteudo>());

            if (!diaCriado.FoiSucesso)
                return diaCriado.Erros;

            diasSemanaConvertidos.Add(diaCriado.Valor!);
        }
        
        var cronogramaCriado = new CronogramaBuilder()
            .ComNome(input.Nome)
            .ComDiasSemana(diasSemanaConvertidos)
            .Builder();
        
        if (!cronogramaCriado.FoiSucesso)
            erros.AddRange(cronogramaCriado.Erros);

        if (erros.Count > 0)
            return erros;

        await cronogramaRepositorio.Salvar(cronogramaCriado.Valor!);
        
        return Mapper.Cronograma(cronogramaCriado.Valor);
    }
    
    public async Task<Resultado<CronogramaDto>> EditarCronograma(Guid id, EditarCronogramaDto dto)
    {   
        var erros = new List<ErroEntidade>();

        var cronograma = await cronogramaRepositorio.BuscarPorId(id);
        
        if (cronograma is null)
        {
            erros.Add(ErroEntidade.CRONOGRAMA_NAO_ENCONTRADO);
            return erros;
        }

        if (!string.IsNullOrWhiteSpace(dto.Nome))
            cronograma.Editar(dto.Nome, cronograma.Dias.ToList());
        
        if (dto.DiasSemana is not null)
        {
            foreach (var diaDto in dto.DiasSemana)
            {
                var dia = cronograma.Dias.FirstOrDefault(d => d.DiaEnum == diaDto.Dia);
                if (dia is null)
                    continue;
                
                
                foreach (var idConteudo in diaDto.ConteudosIds)
                {
                    var conteudo = await conteudoRepositorio.BuscarPorId(idConteudo);

                    if (conteudo is null)
                    {
                        erros.Add(ErroEntidade.CONTEUDO_NAO_ENCONTRADO);
                        continue;
                    }
                    
                    dia.AdicionarConteudo(idConteudo);
                }
                
                var dataCalculada = CalcularDataParaProximoDia(dia.DiaEnum);
                
                var diaEditado = dia.Editar(dia.DiaEnum, dataCalculada);
                
                if(!diaEditado.FoiSucesso)
                    erros.AddRange(diaEditado.Erros);

                if (erros.Count > 0)
                    return erros;
            }
        }
        
        await cronogramaRepositorio.Atualizar(cronograma);

        return Mapper.Cronograma(cronograma);
    }
    
    public async Task<Resultado<CronogramaDto>> BuscarCronogramaPorId(Guid id)
    {   
        var erros = new List<ErroEntidade>();
        
        var cronograma = await cronogramaRepositorio.BuscarPorId(id);

        if (cronograma is null)
        {
            erros.Add(ErroEntidade.CRONOGRAMA_NAO_ENCONTRADO);
            return erros;
        }
        
        return Mapper.Cronograma(cronograma);
    }
    
    public async Task<Resultado<List<CronogramaDto>>> BuscarTodosCronogramas()
    {   
        var erros = new List<ErroEntidade>();
        
        var cronogramas = await cronogramaRepositorio.BuscarTodos();
        
        if (cronogramas is null)
        {
            erros.Add(ErroEntidade.CRONOGRAMA_NAO_ENCONTRADO);
            return erros;
        }
        
        return Mapper.Cronogramas(cronogramas);
    }
    
    public async Task<Resultado<string>> DeletarCronograma(Guid id)
    {   
        var erros = new List<ErroEntidade>();
        
        var cronograma = await cronogramaRepositorio.BuscarPorId(id);
        
        if (cronograma is null)
        {
            erros.Add(ErroEntidade.CRONOGRAMA_NAO_ENCONTRADO);
            return erros;
        }
        
        await cronogramaRepositorio.Remover(cronograma);
        
        return "O cronograma " + cronograma.Nome + " foi deletado com sucesso!";
    }
    
    public async Task<Resultado<CronogramaDto>> AdicionarConteudoAoCronograma(Guid cronogramaId, Guid conteudoId, DiasEnum diaEnum)
    {
        var erros = new List<ErroEntidade>();

        var cronograma = await cronogramaRepositorio.BuscarPorId(cronogramaId);
        if (cronograma is null)
        {
            erros.Add(ErroEntidade.CRONOGRAMA_NAO_ENCONTRADO);
            return erros;
        }

        var conteudo = await conteudoRepositorio.BuscarPorId(conteudoId);
        if (conteudo is null)
        {
            erros.Add(ErroEntidade.CONTEUDO_NAO_ENCONTRADO);
            return erros;
        }

        var dia = cronograma.Dias.FirstOrDefault(d => d.DiaEnum == diaEnum);
        if (dia is null)
        {
            erros.Add(ErroEntidade.DIA_SEMANA_NAO_ENCONTRADO);
            return erros;
        }
        
        dia.AdicionarConteudo(conteudo.Id); 

        await cronogramaRepositorio.Atualizar(cronograma);

        return Mapper.Cronograma(cronograma);
    }
    
    private DateTime CalcularDataParaProximoDia(DiasEnum diaDesejado)
    {
        var hoje = DateTime.Today;
        var diaAtual = (int)hoje.DayOfWeek;
        var diaAlvo = (int)diaDesejado;

        int diasParaAdicionar = (diaAlvo - diaAtual + 7) % 7;
        return hoje.AddDays(diasParaAdicionar == 0 ? 7 : diasParaAdicionar);
    }
    
}