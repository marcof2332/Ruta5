//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class DropOffPackages
    {
        public int IdDropOff { get; set; }
        public int BranchOffice { get; set; }
        public string Note { get; set; }
    
        public virtual BranchOffices BranchOffices { get; set; }
        public virtual Shippments Shippments { get; set; }
    }
}
