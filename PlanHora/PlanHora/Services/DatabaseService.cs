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
            _db.CreateTableAsync<Usuario>().Wait();
        }

        // Métodos para Local
        public Task<List<Local>> GetLocalesAsync() => _db.Table<Local>().ToListAsync();
        public Task<int> SaveLocalAsync(Local local) =>
            local.Id == 0 ? _db.InsertAsync(local) : _db.UpdateAsync(local);
        public Task<List<Local>> GetLocalesPorUsuarioAsync(int usuarioId) =>
            _db.Table<Local>().Where(l => l.UsuarioId == usuarioId).ToListAsync();

        public async Task<int> DeleteLocalAsync(Local local)
        {
            // Obtener los empleados asociados al local
            var empleados = await _db.Table<Empleado>().Where(e => e.LocalId == local.Id).ToListAsync();

            // Obtener los IDs de los empleados para borrar sus horarios
            var empleadoIds = empleados.Select(e => e.Id).ToList();

            // Borrar los horarios de esos empleados
            var horarios = await _db.Table<Horario>().Where(h => empleadoIds.Contains(h.EmpleadoId)).ToListAsync();
            foreach (var h in horarios)
                await _db.DeleteAsync(h);

            // Borrar empleados asociados
            foreach (var emp in empleados)
                await _db.DeleteAsync(emp);

            // borrar el local
            return await _db.DeleteAsync(local);
        }


        // Métodos para Empleado
        public Task<List<Empleado>> GetEmpleadosAsync() => _db.Table<Empleado>().ToListAsync();
        public Task<int> SaveEmpleadoAsync(Empleado empleado) =>
            empleado.Id == 0 ? _db.InsertAsync(empleado) : _db.UpdateAsync(empleado);
        public Task<List<Empleado>> GetEmpleadosPorUsuarioAsync(int usuarioId) =>
            _db.Table<Empleado>().Where(e => e.UsuarioId == usuarioId).ToListAsync();


        // Métodos para Horario
        public Task<List<Horario>> GetHorariosAsync() => _db.Table<Horario>().ToListAsync();
        public Task<int> SaveHorarioAsync(Horario horario) =>
            horario.Id == 0 ? _db.InsertAsync(horario) : _db.UpdateAsync(horario);
       

        // Métodos para Festivo
        public Task<List<Festivo>> GetFestivosAsync() => _db.Table<Festivo>().ToListAsync();
        public Task<int> SaveFestivoAsync(Festivo festivo) =>
            festivo.Id == 0 ? _db.InsertAsync(festivo) : _db.UpdateAsync(festivo);


        // Métodos para usuario
        public async Task<int> SaveUsuarioAsync(Usuario usuario)
        {
            var existente = await _db.Table<Usuario>()
                .Where(u => u.NombreUsuario == usuario.NombreUsuario)
                .FirstOrDefaultAsync();

            if (existente != null)
                throw new Exception("El usuario ya existe.");

            return await _db.InsertAsync(usuario);
        }


        public Task<Usuario> GetUsuarioAsync(string nombreUsuario, string contrasena)
        {
            return _db.Table<Usuario>()
                      .Where(u => u.NombreUsuario == nombreUsuario && u.Contrasena == contrasena)
                      .FirstOrDefaultAsync();
        }

        public Task<int> DeleteEmpleadoAsync(Empleado empleado)
        {
            return _db.DeleteAsync(empleado);
        }


    }
}
