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
    [Serializable]
    class Quests
    {
        public string questName;
        public string dueDate;
        public string difficulty;

        public Quests(string name, string due, string diff)
        {
            this.questName = name;
            this.dueDate = due;
            this.difficulty = diff;
        }
    }
}