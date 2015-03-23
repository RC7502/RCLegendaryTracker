using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Excel;
using LegendaryTracker.Interfaces;
using LegendaryTracker.Models;

namespace LegendaryTracker.Services
{
    public class ExcelDAL : IDAL
    {
        public FileStream stream;
        public IExcelDataReader excel;
        public string path = HttpContextFactory.Current.Server.MapPath("~/Files/") + "RCLTDB.xlsx";

        public ExcelDAL()
        {
            stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            excel = ExcelReaderFactory.CreateOpenXmlReader(stream);
            excel.IsFirstRowAsColumnNames = true;
        }


        public User GetUser(string username)
        {
            var user = new User();
            try
            {
                var usertable = excel.AsDataSet().Tables["users"];
                if (usertable.Rows.Count > 0)
                {
                    var row = usertable.Select(string.Format("username = '{0}'", username))[0];
                    user.UserName = row["username"].ToString();
                    user.Password = row["password"].ToString();
                    user.Email = row["email"].ToString();
                    return user;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public List<SetModel> GetSets()
        {
            var list = new List<SetModel>();
            try
            {
                var settable = excel.AsDataSet().Tables["sets"];
                foreach (DataRow row in settable.Rows)
                {
                    list.Add(new SetModel
                        {
                            Id = row["id"].ToString(),
                            Name = row["name"].ToString(),
                            SortOrder = Convert.ToDouble(row["sortorder"])
                        });
                }
            }
            catch (Exception ex)
            {
                return list;
            }
            return list.OrderBy(x=>x.SortOrder).ToList();
        }

        public List<CardModel> GetCards()
        {
            var list = new List<CardModel>();
            try
            {
                var settable = excel.AsDataSet().Tables["cards"];
                foreach (DataRow row in settable.Rows)
                {
                    var card = new CardModel
                    {
                        Id = new Guid(row["id"].ToString()),
                        Type = row["type"].ToString(),
                        Name = row["name"].ToString(),
                        AlwaysLeads = row["alwaysleads"].ToString() == "" ? Guid.Empty : new Guid(row["alwaysleads"].ToString()),
                        Set = row["set"].ToString(),
                        TotalQty = Convert.ToDouble(row["totalqty"])
                    };
                    var set = GetSets().FirstOrDefault(x => x.Id == card.Set);
                    card.FullName = set != null ? string.Format("{0} ({1})", card.Name, set.Name) : card.Name;
                    list.Add(card);
                }
            }
            catch (Exception ex)
            {
                return list;
            }
            return list;
        }
    }
}