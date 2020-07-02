using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using System.Configuration;
using System.Data;

namespace BibliotecaDALC
{
    public class Conexion
    {
        private string cadena = "Data Source = localhost:1521/XE;User Id = OKCasa; Password=OKCasa";
        //desde que lugar me conecto//el usuario//la pass

        private static OracleConnection conn;

        public Conexion()
        {
            if (conn == null)
            {
                conn = new OracleConnection(cadena);
            }
        }

        public OracleConnection Getcone()
        {
            return conn;
        }
    }
}
