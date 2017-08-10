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
    
    public partial class Siniestro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Siniestro()
        {
            this.DetalleGasto = new HashSet<DetalleGasto>();
        }
    
        public int idSiniestro { get; set; }
        public Nullable<System.DateTime> fechaInicio { get; set; }
        public string Lugar { get; set; }
        public string Detalle { get; set; }
        public string dañosMateriales { get; set; }
        public string dañosPersonales { get; set; }
        public Nullable<System.DateTime> fechaFin { get; set; }
        public byte[] Fotografia { get; set; }
        public Nullable<double> Costo { get; set; }
        public string Estado { get; set; }
        public string DescripcionCierre { get; set; }
        public Nullable<int> idPoliza { get; set; }
        public Nullable<int> idFuncionario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleGasto> DetalleGasto { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual polizaVehicular polizaVehicular { get; set; }
    }
}
