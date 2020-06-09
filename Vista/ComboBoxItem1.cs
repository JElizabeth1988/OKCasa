namespace Vista
{
    internal class comboBoxItem1
    {
        /*Para clases:
            -Art_sanitario
            -Agrupamiento???????-----> revisar
            -Comuna
            -Entidad_Bancaria
            -Equipo_tecnico
            -Inst_agua_potable
            -ist_alcantarillado
            -inst_electrica
            -inst_gas
            -insumo
            -Red_agua
            -tipo_pago
            -Tipo_vivienda*/
        public int id { get; set; }
        public string nombre { get; set; }


        public comboBoxItem1()
        {

        }

        public override string ToString()
        {
            return nombre;
        }
    }
}
