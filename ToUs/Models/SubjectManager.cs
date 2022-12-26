//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToUs.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubjectManager
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubjectManager()
        {
            this.TimeTables = new HashSet<TimeTable>();
        }
    
        public int Id { get; set; }
        public string SubjectId { get; set; }
        public string TeacherId { get; set; }
        public string ClassId { get; set; }
        public bool IsDelete { get; set; }
        public string ExcelPath { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTable> TimeTables { get; set; }
    }
}
