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
    [Activity(Label = "MainActivity", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button addQuest;
        //DB path
        static string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "QuestsDb.db3");
        //DB variable
        SQLiteConnection db = new SQLiteConnection(path);
        Quest[] questList;

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
                db.CreateTable<Quest>();
            }
            else
            {
                return;
            }
        }

        //Method to load quest list
        public void LoadQuestList()
        {
            //Array to hold quests
            questList = new Quest[30];

            //Populate array from db
            var table = db.Table<Quest>();
            int count = 0;
            foreach (var i in table)
            {
                questList[count] = i;
                count++;
            }

            //Create list view for quests
            ListView questsView = FindViewById<ListView>(Resource.Id.quests);
            questsView.Adapter = new QuestListAdapter(this, questList);

            //Register list view for context menu
            RegisterForContextMenu(questsView);
        }

        //Method for creating context menu
        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            if(v.Id == Resource.Id.quests)
            {
                var info = (AdapterView.AdapterContextMenuInfo)menuInfo;
                menu.SetHeaderTitle("Quest Menu");
                var menuItems = Resources.GetStringArray(Resource.Array.menu);
                for(int i = 0; i < menuItems.Length; i++)
                {
                    menu.Add(Menu.None, i, i, menuItems[i]);
                }
            }
        }

        //Method for handling when menu item is selected
        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var index = item.ItemId;
            var menuItem = Resources.GetStringArray(Resource.Array.menu);
            var menuItemName = menuItem[index];
            var questName = questList[info.Position];

            //Check if complete was selected
            if (menuItemName == "Complete")
            {


                //Delete quest from db
                db.Delete(questName);
            }

            return true;
        }
    }
}
