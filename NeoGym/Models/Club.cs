//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NeoGym.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Club
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public Nullable<int> price { get; set; }
        public string meta { get; set; }
        public Nullable<bool> hide { get; set; }
        public Nullable<int> order { get; set; }
        public Nullable<System.DateTime> datebegin { get; set; }
    }
}
