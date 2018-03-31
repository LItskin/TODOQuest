using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

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

        //Method for sotring user input
        private void SubmitButtonClick()
        {

        }
    }
}