//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductInTopic
    {
        public int ID { get; set; }
        public long TopicID { get; set; }
        public long ProductID { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Topic Topic { get; set; }
    }
}