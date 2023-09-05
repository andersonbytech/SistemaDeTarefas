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
            return await _dbcontext.Tarefas
                .Include( x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbcontext.Tarefas
                .Include ( x => x.Usuario)
                .ToListAsync();
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
            TarefaModel tarefaPorId = await BuscarPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado.");

            }
            _dbcontext.Tarefas.Remove(tarefaPorId);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
