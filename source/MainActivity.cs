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
    [Activity(Label = "MainActivity")]
    public class MainActivity : Activity
    {
        Button addQuest;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Open Main Menu
            SetContentView(Resource.Layout.Main);

            addQuest = FindViewById<Button>(Resource.Id.addButton);
            addQuest.Click += delegate
            {
                StartActivity(typeof(AddQuest));
            };
        }
    }
}