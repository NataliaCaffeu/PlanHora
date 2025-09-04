using PlanHora.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanHora.Services
{
    internal class DatabaseService
    {
        private SQLiteConnection _database;

        public DatabaseService()
        {
            // Definir la ruta del archivo de base de datos local
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "planhora.db3");

            // Crear la conexión
            _database = new SQLiteConnection(dbPath);

            // Crear tablas si no existen
            _database.CreateTable<Local>();
            _database.CreateTable<Empleado>();
            _database.CreateTable<Horario>();
            _database.CreateTable<Festivo>();

        }

        // Métodos para Local 
        public List<Local> GetLocales() => _database.Table<Local>().ToList();
        public void AddLocal(Local local) => _database.Insert(local);
        public void UpdateLocal(Local local) => _database.Update(local);
        public void DeleteLocal(Local local) => _database.Delete(local);

        // Métodos para Empleado 
        public List<Empleado> GetEmpleados() => _database.Table<Empleado>().ToList();
        public void AddEmpleado(Empleado empleado) => _database.Insert(empleado);
        public void UpdateEmpleado(Empleado empleado) => _database.Update(empleado);
        public void DeleteEmpleado(Empleado empleado) => _database.Delete(empleado);

        // Métodos para Horario 
        public List<Horario> GetHorarios() => _database.Table<Horario>().ToList();
        public void AddHorario(Horario horario) => _database.Insert(horario);
        public void UpdateHorario(Horario horario) => _database.Update(horario);
        public void DeleteHorario(Horario horario) => _database.Delete(horario);

        // Métodos para Festivo 
        public List<Festivo> GetFestivos() => _database.Table<Festivo>().ToList();
        public void AddFestivo(Festivo festivo) => _database.Insert(festivo);
        public void UpdateFestivo(Festivo festivo) => _database.Update(festivo);
        public void DeleteFestivo(Festivo festivo) => _database.Delete(festivo);
    }
}
