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
    
    public partial class Funcionario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Funcionario()
        {
            this.polizaVehicular = new HashSet<polizaVehicular>();
            this.Siniestro = new HashSet<Siniestro>();
        }
    
        public int idFuncionario { get; set; }
        public string Nombre { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int idRol { get; set; }
    
        public virtual Roles Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<polizaVehicular> polizaVehicular { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siniestro> Siniestro { get; set; }
    }
}