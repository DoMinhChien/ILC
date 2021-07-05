//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class HotelLocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HotelLocation()
        {
            this.Hotels = new HashSet<Hotel>();
        }
    
        public System.Guid Id { get; set; }
        public Nullable<int> City_Id { get; set; }
        public Nullable<int> Ward_Id { get; set; }
        public Nullable<int> District_Id { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
    
        public virtual City City { get; set; }
        public virtual District District { get; set; }
        public virtual Ward Ward { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}