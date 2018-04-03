using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace TODOQuestApp
{
    [Activity(Label = "MainActivity", Icon = "@drawable/icon", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button addQuest;
        //DB path
        static string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "QuestsDb.db3");
        //DB variable
        SQLiteConnection db = new SQLiteConnection(path);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            //Call method to create tables if it doesn't exist
            DBCheckAndCreate();

            //Open Main Menu
            SetContentView(Resource.Layout.Main);

            //Call method to load the quest list
            LoadQuestList();

            addQuest = FindViewById<Button>(Resource.Id.addButton);
            addQuest.Click += delegate
            {
                StartActivity(typeof(AddQuest));
            };
        }

        //Method to create db if it doesn't exist
        private void DBCheckAndCreate()
        {
            var info = db.GetTableInfo("Quests");
            if (!info.Any())
            {
                db.CreateTable<Quests>();
            }
            else
            {
                return;
            }
        }

        //Method to load quest list
        public void LoadQuestList()
        {
            
        }
    }
}
