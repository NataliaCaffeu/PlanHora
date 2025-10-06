using PlanHora.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace PlanHora.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _db;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "planhora.db3");
            _db = new SQLiteAsyncConnection(dbPath);

            // Crear tablas si no existen
            _db.CreateTableAsync<Local>().Wait();
            _db.CreateTableAsync<Empleado>().Wait();
            _db.CreateTableAsync<Horario>().Wait();
            _db.CreateTableAsync<Festivo>().Wait();
        }

        // Métodos para Local
        public Task<List<Local>> GetLocalesAsync() => _db.Table<Local>().ToListAsync();
        public Task<int> SaveLocalAsync(Local local) =>
            local.Id == 0 ? _db.InsertAsync(local) : _db.UpdateAsync(local);

        // Métodos para Empleado
        public Task<List<Empleado>> GetEmpleadosAsync() => _db.Table<Empleado>().ToListAsync();
        public Task<int> SaveEmpleadoAsync(Empleado empleado) =>
            empleado.Id == 0 ? _db.InsertAsync(empleado) : _db.UpdateAsync(empleado);

        // Métodos para Horario
        public Task<List<Horario>> GetHorariosAsync() => _db.Table<Horario>().ToListAsync();
        public Task<int> SaveHorarioAsync(Horario horario) =>
            horario.Id == 0 ? _db.InsertAsync(horario) : _db.UpdateAsync(horario);

        // Métodos para Festivo
        public Task<List<Festivo>> GetFestivosAsync() => _db.Table<Festivo>().ToListAsync();
        public Task<int> SaveFestivoAsync(Festivo festivo) =>
            festivo.Id == 0 ? _db.InsertAsync(festivo) : _db.UpdateAsync(festivo);
    }
}
