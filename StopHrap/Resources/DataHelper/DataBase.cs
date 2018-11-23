using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite;
using StopHrap.Resources.Model;

namespace StopHrap.Resources.DataHelper
{
    public class DataBase
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDataBase(SettingsModel defaultSettings = null)
        {
            try
            {
                using(var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StopHrap.db")))
                {
                    var result = connection.CreateTable<SettingsModel>();
                    if(result == CreateTableResult.Created)
                    {
                        InsertIntoTableSettigns(defaultSettings);
                    }
                    return true;
                }
                
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool InsertIntoTableSettigns(SettingsModel settings)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StopHrap.db")))
                {
                    connection.Insert(settings);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<SettingsModel> SelectTableSettigns()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StopHrap.db")))
                {
                    return connection.Table<SettingsModel>().ToList();
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool UpdateTableSettigns(SettingsModel settings)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StopHrap.db")))
                {
                    connection.Update(settings);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool DeleteIntoTableSettigns(SettingsModel settings)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StopHrap.db")))
                {
                    connection.Delete(settings);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
    }
}