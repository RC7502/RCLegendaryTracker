using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LegendaryTracker.Models
{
    public class RandomizerOptionsModel
    {
        public List<CheckBoxItem> Sets { get; set; }
        public List<SelectListItem> NumOfPlayers { get; set; }
        public int SelectedNumPlayers { get; set; }

        public RandomizerOptionsModel()
        {
            Sets = new List<CheckBoxItem>();
            NumOfPlayers = new List<SelectListItem>();
        }
    }
}