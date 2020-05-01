using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public enum Comuna
    {
        Cerrillos, Cerro_Navia, Conchalí, El_Bosque, Estación_Central, Huechuraba,
        Independencia, La_Cisterna, La_Florida, La_Granja, La_Pintana, La_Reina,
        Las_Condes, Lo_Barnechea, Lo_Espejo, Lo_Prado, Macúl, Ñuñoa, Padre_Hurtado,
        Pedro_Aguirre_Cerda, Peñaflor, Peñalolen, Providencia, Pudahuel, Puente_Alto,
        Quilicura, Quinta_Normal, Recoleta, Renca, San_Bernardo, San_Joaquín, San_Miguel,
        San_Ramón, Santiago, Vitacura
    }

    public enum TipoVivienda
    {
        Casa, Departamento, Otro
    }

    public enum TipoAgrupamiento
    {
        Pareada, Aislada, Continua, Otro
    }

    public enum Habitaciones
    {
        _1, _2 , _3, _4, _5, _6, _7, _8, _9, _10, Otro
    }

    public enum Pisos
    {
        _1, _2, _3, _4, _5, Otro
    }

    public enum Herramientas
    {
        Analizador_de_CO,
        Antiparras,
        Calibre,
        Nivel,
        Cámara_Infrarroja,
        Cinta_Métrica,
        Detector_Gas_Combustible,
        Escalera_Telescópica,
        Espectoscopio,
        Guantes,
        Indicador_Voltaje,
        Kit_Herramientas_Básicas,
        Linterna,
        Medidor_Humedad,
        Monitor_de_Radón,
        Plomada,
        Probador_AFCI,
        Probador_Eléctrico,
        Respirador,
        Termómetro_Infrarrojo

    }

    public enum InstAgua
    {
        Cañerias_PVC, Cañerias_Cobre, Gasfitería_Corriente, Gasfitería_Especial, Otro
    }

    public enum RedAgua
    {
       Sistema_Red_Pública, Sistema_Individual_Pozo, Sistema_Individual_Puntería, Sistema_Privado_Colectivo, Otro
    }
    public enum Alcantarillado
    {
        Tubería_PVC, Tubería_Cemento, Colector_Público, Fosa_Séptica, Pozo, Otro
    }
    public enum Sanitario
    {
        Económico, Corriente, Especial, Otro
    }
    public enum Electrica
    {
        Canalización_PVC_Embutido, Canalización_PVC_a_la_Vista, Sin_Canalización, Artefacto_Económico, Artefacto_Especial, Otro
    }
    public enum Gas
    {
        Conexión_a_Red_Pública, Sistema_Individual, No_Tiene, Otro
    }


    public class Formulario
    {
    }
}
