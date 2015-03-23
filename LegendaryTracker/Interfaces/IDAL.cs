using System.Collections.Generic;
using LegendaryTracker.Models;

namespace LegendaryTracker.Interfaces
{
    public interface IDAL
    {
        User GetUser(string username);
        List<SetModel> GetSets();
        List<CardModel> GetCards();
    }
}
