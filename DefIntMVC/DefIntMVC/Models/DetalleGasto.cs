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
    
    public partial class DetalleGasto
    {
        public int idDetalleGasto { get; set; }
        public Nullable<double> Sepelio { get; set; }
        public byte[] imagenSepelio { get; set; }
        public Nullable<double> atencionMedica { get; set; }
        public byte[] imagenAtMedic { get; set; }
        public Nullable<double> dañosPropiedad { get; set; }
        public byte[] imagenDProp { get; set; }
        public Nullable<double> muerteAccidente { get; set; }
        public byte[] imagenMuerteAcc { get; set; }
        public Nullable<double> perdidaTotal { get; set; }
        public byte[] imagenPerdTotal { get; set; }
        public Nullable<double> reparacionVehicular { get; set; }
        public byte[] imagenRepVehicular { get; set; }
        public int idSiniestro { get; set; }
    
        public virtual Siniestro Siniestro { get; set; }
    }
}