//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOO_ChemistShop.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.MedicineOrder = new HashSet<MedicineOrder>();
        }
    
        public int OrderId { get; set; }
        public System.DateTime DateOrder { get; set; }
        public System.DateTime DateDelivery { get; set; }
        public int OrderPointId { get; set; }
        public string OrderClient { get; set; }
        public int OrderCode { get; set; }
        public int OrderStatusId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicineOrder> MedicineOrder { get; set; }
        public virtual Point Point { get; set; }
        public virtual Status Status { get; set; }
    }
}
