//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DefIntMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class View_Report3
    {
        public int IDSiniestro { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FichaFin { get; set; }
        public string Lugar { get; set; }
        public string Detalle { get; set; }
        public string DañosMateriales { get; set; }
        public string DañosPersonales { get; set; }
        public byte[] Fotografia { get; set; }
        public Nullable<double> Costo { get; set; }
        public string Estado { get; set; }
        public string DescripcionCierre { get; set; }
    }
}