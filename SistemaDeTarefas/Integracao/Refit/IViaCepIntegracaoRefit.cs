using Refit;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SistemaDeTarefas.Integracao.Response;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeTarefas.Integracao.Refit
{
    public interface IViaCepIntegracaoRefit
    {
        [Get("/ws/{cep}/json")]
        Task<ApiResponse<ViaCepResponse>> ObterDadosViaCep(string Cep);



    }
}
