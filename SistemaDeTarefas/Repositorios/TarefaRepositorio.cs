using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;
using System.Data;

namespace SistemaDeTarefas.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {

        private readonly SistemaDeTarefasDbContext _dbcontext;
        public TarefaRepositorio(SistemaDeTarefasDbContext sistemaDeTarefasDbContext)
        {
            _dbcontext = sistemaDeTarefasDbContext;
        }

        public async Task<TarefaModel> BuscarPorId(int id)
        {
            return await _dbcontext.Tarefas.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbcontext.Tarefas.ToListAsync();
        }
        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _dbcontext.Tarefas.AddAsync(tarefa);
           await _dbcontext.SaveChangesAsync();
            return tarefa;

        }
        
        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrada.");               

            }

            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Descricao = tarefa.Descricao;
            tarefaPorId.Status = tarefa.Status;
            tarefaPorId.UsuarioId = tarefa.UsuarioId;

            _dbcontext.Update(tarefaPorId);
            await _dbcontext.SaveChangesAsync();
            return tarefaPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o ID: {id} não foi encontrado.");

            }

            _dbcontext.Usuarios.Remove(usuarioPorId);
            await _dbcontext.SaveChangesAsync();
            return true;


        }

    }
}
