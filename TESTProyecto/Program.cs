using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaNegocio;

namespace TESTProyecto
{
    class Program
    {
        static void Main(string[] args)
        {
            Comuna c = new Comuna();

            List<Comuna> lista = c.ReadAll();

            foreach (var item in lista)
            {
                Console.WriteLine(item.nombre);
            }
            Console.ReadKey();
        }
    }
}
