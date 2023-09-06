using SistemaDeTarefas.Integracao.Response;

namespace SistemaDeTarefas.Integracao.Interface
{
    public interface IViaCepIntegracao
    {
        Task<ViaCepResponse> ObterDadosViaCep(string cep);
    }
}
