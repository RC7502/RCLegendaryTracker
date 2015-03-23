using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegendaryTracker.Models
{
    public class CardModel
    {
        public Guid Id;
        public string Type;
        public string Name;
        public Guid AlwaysLeads;
        public string Set;
        public double TotalQty;
        public string FullName;
    }
}