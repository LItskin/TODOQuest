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
    [Activity(Label = "AddQuest")]
    public class AddQuest : Activity, DatePickerDialog.IOnDateSetListener
    {
        string QUEST_NAME;
        string DUE_DATE;
        string DIFFICULTY;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Open Add Quest UI
            SetContentView(Resource.Layout.AddQuest);

            //User Entry Variables
            var questNameEntry = FindViewById<EditText>(Resource.Id.questNameEnter);
            var dueDateEntry = FindViewById<EditText>(Resource.Id.dueDateEntry);
            Spinner difficultySelect = FindViewById<Spinner>(Resource.Id.difficultyEntry);
            Button submit = FindViewById<Button>(Resource.Id.submitButton);

            //Spinner setup
            difficultySelect.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(difficultySelect_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.difficulties_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerItem);
            difficultySelect.Adapter = adapter;

            //Quest Name entry handler
            questNameEntry.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                QUEST_NAME = e.ToString();
                questNameEntry.Text = QUEST_NAME;
            };

            //Date entry handler
            dueDateEntry.Click += delegate
            {
                OnClickDueDateEntry();
            };

            //Submit button handler
            submit.Click += delegate
            {
                SubmitButtonClick();
            };
        }

        //Method for opening date dialog
        public void OnClickDueDateEntry()
        {
            var dateTimeNow = DateTime.Now;
            DatePickerDialog datePick = new DatePickerDialog(this, this, dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day);
            datePick.Show();
        }

        //Method for storing date from date dialog
        public void OnDateSet(DatePicker view, int year, int month, int day)
        {
            DUE_DATE = new DateTime(year, month, day).ToShortDateString();
            FindViewById<EditText>(Resource.Id.dueDateEntry).Text = DUE_DATE;
        }

        //Method for storing difficulty
        private void difficultySelect_ItemSelected(Object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            DIFFICULTY = (string)spinner.GetItemAtPosition(e.Position);
        }

        //Method for storing user input
        private void SubmitButtonClick()
        {
            //bool variable for table check
            bool isIn = false;
            Quests quest = new Quests(QUEST_NAME, DUE_DATE, DIFFICULTY);

            //Get db path file path
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "QuestsDb.db3");
            SQLiteConnection db = new SQLiteConnection(path);

            //Check if object exists in table          
            var table = db.Table<Quests>();
            foreach(var i in table) //Check records in table
            {
                if(i.questName == quest.questName)
                {
                    isIn = true;
                    break;
                }
                else
                {
                    isIn = false;
                }
            }
            if(isIn == true) //Check bool variable
            {
                //Alert if the quest already exists
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Error");
                alert.SetMessage("This quest already exists. Please enter a new quest.");
                alert.SetNeutralButton("Ok", delegate
                {
                    alert.Dispose();
                });
            }
            else
            {
                db.Insert(quest);
            }

            db.Close();
            //Go back to main screen
            StartActivity(typeof(MainActivity));
        }
    }
}
