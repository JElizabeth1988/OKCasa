using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Agenda
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();
        //Capturar Errores
        DaoErrores err = new DaoErrores();
        public DaoErrores retornar() { return err; }

        //Creacion de los atributos
        public int id_agenda { get; set; }
        public DateTime dia { get; set; }
        public string hora { get; set; }
        public string disponible { get; set; }
        public int id_equipo { get; set; }


        public Agenda()
        {

        }

        /*//CRUD
        //Guardar
        public Boolean Guardar()
        {
            try
            {
                //creo un modelo de la tabla agenda
                AGENDA ag = new AGENDA();
                CommonBC.Syncronize(this, ag);
                bdd.AGENDA.Add(ag);
                bdd.SaveChanges();

                return true;


            }
            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);
            }
        }

        //Eliminar
        public bool Eliminar()
        {
            try
            {
                AGENDA ag =
                bdd.AGENDA.Find(id_agenda);

                bdd.AGENDA.Remove(ag);
                bdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);
            }
        }

        //Buscar
        public bool Buscar()
        {
            try
            {
                AGENDA ag =
                bdd.AGENDA.First(age => age.ID_AGENDA.Equals(id_agenda));

                CommonBC.Syncronize(ag, this);//arregló this

                return true;

            }
            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);
            }
        }

        //Modificar
        public bool Modificar()
        {
            try
            {
                //creo un modelo de la tabla Agenda
                AGENDA ag = bdd.AGENDA.Find(id_agenda);
                CommonBC.Syncronize(this, ag);
                bdd.SaveChanges();
                return true;


            }
            catch (Exception ex)
            {

                return false;
            }
        }

        //Read
        public bool Read()
        {
            try
            {
                AGENDA ag = bdd.AGENDA.Find(id_agenda);
                CommonBC.Syncronize(ag, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }
        public List<Agenda> ReadAll()
        {
            try
            {
                var c = from age in bdd.AGENDA
                        select new Agenda()
                        {
                            id_agenda = age.ID_AGENDA,
                            dia = age.DIA,
                            hora = age.HORA,
                            id_equipo = age.ID_EQUIPO
                            
                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        //Read all 2 
        public List<ListaAgenda> ReadAll2()
        {
            try
            {
                var c = from age in bdd.AGENDA
                        join equi in bdd.EQUIPO_TECNICO //Join con Equipo para traer el nombre y no su id igualando el dato en común (id_equipo)
                          on age.ID_EQUIPO equals equi.ID_EQUIPO
                        select new ListaAgenda()
                        {
                            //Comparo datos de la lista
                            Id = age.ID_AGENDA,
                            Dia = age.DIA,
                            Hora = age.HORA,
                            Equipo = equi.NOMBRE//Traigo el nombre no el id

                        };
                return c.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }*/

        

        //Para retornar datos sin id
        public class ListaAgenda
        {
            public int Id { get; set; }
            public string Fecha { get; set; }
            public string Hora { get; set; }
            public string Disponibilidad { get; set; }
            public string Equipo { get; set; }
           

            public ListaAgenda()
            {

            }
        }

    }

}
