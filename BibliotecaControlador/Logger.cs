using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; //ENTRADA Y SALIDA

namespace BibliotecaControlador
{
    public class Logger
    {
        public static void Mensaje(String msg)
        {
            msg = DateTime.Now + " | " + msg + Environment.NewLine;
            File.AppendAllText(@"c:\logger.txt", msg);

            //Environment.NewLine: cambio de linea
            //DateTime.Now: fecha actual
        }
    }
}
