using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E17561.Models
{
   public  class Localizacion
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        public double latitud { get; set; }
        public double longitud { get; set; }

        [MaxLength(200)]
        public string descripcion_larga { get; set; }

        [MaxLength(100)]
        public string descripcion_corta { get; set; }

    }
}
