using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaNegocio
{
    public class Verificar
    {
        public string ValidarRut(string rutParaValidar)
        {
            string conf = "";

            /*Quitar los . las , y -*/
            rutParaValidar = rutParaValidar.Replace(",", "");
            rutParaValidar = rutParaValidar.Replace(".", "");
            rutParaValidar = rutParaValidar.Replace("-", "");

            /*ajustar el largo del rut a validar si es de 7 se agrega un 0 antes para validar largo*/
            if (rutParaValidar.Length == 7)
            {
                rutParaValidar = "0" + rutParaValidar;
            }

            //recatar los 8 digitos (ya se agregó cero en caso de 7 dígitos)
            int a1 = int.Parse(rutParaValidar.Substring(0, 1));
            int a2 = int.Parse(rutParaValidar.Substring(1, 1));
            int a3 = int.Parse(rutParaValidar.Substring(2, 1));
            int a4 = int.Parse(rutParaValidar.Substring(3, 1));
            int a5 = int.Parse(rutParaValidar.Substring(4, 1));
            int a6 = int.Parse(rutParaValidar.Substring(5, 1));
            int a7 = int.Parse(rutParaValidar.Substring(6, 1));
            int a8 = int.Parse(rutParaValidar.Substring(7, 1));

            /*rescatar Verificador a comparar*/
            //string dv9 = rutParaValidar.Substring(7, 1);

            //Fijar un número en una letra.(Constante).
            int b1 = 8;
            int b2 = 9;
            int b3 = 4;
            int b4 = 5;
            int b5 = 6;
            int b6 = 7;
            int b7 = 8;
            int b8 = 9;

            //Multiplicas cada DIGITO con las CONSTANTES creadas.
            int a = (a1 * b1) + (a2 * b2) + (a3 * b3) + (a4 * b4) + (a5 * b5) + (a6 * b6) + (a7 * b7) + (a8 * b8);

            //PD: Trunc aproxima siempre al valor inferior.
            double resultado = (a / 11);
            double resultado1 = Math.Truncate(resultado);

            //Realiza operacion para obtener digito.
            double b = resultado1 * 11;

            double dv = a - b;

            //Digito Verificador Asignado.
            if (dv == 10)
            {
                conf = "K";
            }
            else
            {
                conf = "" + dv;
            }

            return conf;
        }
    }
}
