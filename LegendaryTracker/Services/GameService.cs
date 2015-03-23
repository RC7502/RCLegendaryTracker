using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LegendaryTracker.Interfaces;
using LegendaryTracker.Models;
using LegendaryTracker.Helpers;

namespace LegendaryTracker.Services
{
    class GameService
    {
        private readonly IDAL _dal;
        private List<CardModel> _cardList; 

        public GameService(IDAL dal)
        {
            _dal = dal;
        }

        public RandomizerOptionsModel InitOptions()
        {
            var model = new RandomizerOptionsModel();
            var sets = _dal.GetSets();
            foreach (var set in sets)
            {
                model.Sets.Add(new CheckBoxItem {Id=set.Id,Name = set.Name,Selected = false});
            }

            model.NumOfPlayers = new List<SelectListItem>
                {
                    new SelectListItem{Text = "1",Value = "1"},
                    new SelectListItem{Text = "2",Value = "2"},
                    new SelectListItem{Text = "3",Value = "3"},
                    new SelectListItem{Text = "4",Value = "4"},
                    new SelectListItem{Text = "5",Value = "5"},
                };


            return model;
        }

        public RandomizerResultModel Randomize(RandomizerOptionsModel options)
        {
            var model = new RandomizerResultModel();
            _cardList = _dal.GetCards()
                            .Where(x => options.Sets.Where(y => y.Selected).Select(z => z.Id).Contains(x.Set)).ToList();

            model.Mastermind = GetMastermind();
            model.Heroes = GetHeroes(options.SelectedNumPlayers);
            model.VillainGroups = GetVillainGroups(options.SelectedNumPlayers, model.Mastermind);
            model.HenchmenGroups = GetHenchmenGroups(options.SelectedNumPlayers, model.Mastermind);
            model.Scheme = GetScheme(options.SelectedNumPlayers);

            return model;
        }

        private CardModel GetScheme(int selectedNumPlayers)
        {
            var schemes = _cardList.Where(x => x.Type == "schemes").ToList();
            schemes.RemoveAll(x => Helper.RestrictedSchemeNames(selectedNumPlayers).Contains(x.Name));
            return Helper.GetRandomItem(schemes);
        }

        private List<CardModel> GetHenchmenGroups(int selectedNumPlayers, CardModel mastermind)
        {
            var list = new List<CardModel>();
            var henchmen = _cardList.Where(x => x.Type == "henchmengroup").ToList();
            if (selectedNumPlayers > 1)
            {
                var alwaysleads = henchmen.FirstOrDefault(x => x.Id == mastermind.AlwaysLeads);
                if (alwaysleads != null)
                {
                    list.Add(alwaysleads);
                    henchmen.Remove(alwaysleads);
                }
            }
            while (list.Count < Helper.HenchmenGroupCount(selectedNumPlayers))
            {
                list.Add(Helper.GetRandomItem(henchmen));
            }

            return list;
        }

        private List<CardModel> GetVillainGroups(int selectedNumPlayers, CardModel mastermind)
        {
            var list = new List<CardModel>();
            var villaincards = _cardList.Where(x => x.Type == "villaingroup").ToList();
            if (selectedNumPlayers > 1)
            {
                var alwaysleads = villaincards.FirstOrDefault(x => x.Id == mastermind.AlwaysLeads);
                if (alwaysleads != null)
                {
                    list.Add(alwaysleads);
                    villaincards.Remove(alwaysleads);
                }
            }
            while (list.Count < Helper.VillainsGroupCount(selectedNumPlayers))
            {
                list.Add(Helper.GetRandomItem(villaincards));
            }

            return list;
        }

        private List<CardModel> GetHeroes(int selectedNumPlayers)
        {
            var list = new List<CardModel>();
            var herocards = _cardList.Where(x => x.Type == "heroes").ToList();
            list.Add(Helper.GetRandomItem(herocards));
            list.Add(Helper.GetRandomItem(herocards));
            list.Add(Helper.GetRandomItem(herocards));
            if (selectedNumPlayers > 1)
            {
                list.Add(Helper.GetRandomItem(herocards));
                list.Add(Helper.GetRandomItem(herocards));
            }
            if(selectedNumPlayers == 5)
                list.Add(Helper.GetRandomItem(herocards));

            return list;
        }

        private CardModel GetMastermind()
        {
            return Helper.GetRandomItem(_cardList.Where(x => x.Type == "mastermind").ToList());
        }


    }
}
