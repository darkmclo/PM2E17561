using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PM2E17561.Models;
using SQLite;

namespace PM02PG02.Controller
{
   public class DataBaseSQLite
    {

        readonly SQLiteAsyncConnection db;
 
        //constructor de la clase DataBaseSQLite
        public DataBaseSQLite(string pathdb)
        {
             db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<Localizacion>().Wait();
        }

        //Operaciones crud de sqlite
        //Read List way
        public Task<List<Localizacion>> ObtenerListaLocalizacion()
        {
            return db.Table<Localizacion>().ToListAsync();
        }

        //read one by one 
        public Task<Localizacion> ObtenerLocalizacion(int pcodigo)
        {
            return db.Table<Localizacion>()
                .Where(i => i.codigo == pcodigo)
                .FirstOrDefaultAsync();
        }

        //Create o update personas
        public Task<int> GrabarLocalizacion(Localizacion localizacion)
        {
            if (localizacion.codigo != 0)
            {
               return db.UpdateAsync(localizacion);
            }
            else
            {
                return db.InsertAsync(localizacion);
            }
            
        }

  

        //delete
        public Task<int> EliminarLocalizacion(Localizacion localizacion)
        {
            return db.DeleteAsync(localizacion);
        }


    }
}
