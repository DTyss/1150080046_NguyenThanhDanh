//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Danh.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DienKe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DienKe()
        {
            this.HoaDons = new HashSet<HoaDon>();
        }
    
        public string MaDienKe { get; set; }
        public string HieuDienThe { get; set; }
        public string NuocSanXuat { get; set; }
        public string GhiChu { get; set; }
        public string MaKhuVuc { get; set; }
        public string MaKH { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
        public virtual KhuVuc KhuVuc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
