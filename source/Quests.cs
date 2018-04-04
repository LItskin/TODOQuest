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
using SQLite;

namespace TODOQuestApp
{
    [Serializable]
    class Quest
    {
        [PrimaryKey, AutoIncrement]
        private int questId { get; set; }
        public string questName;
        public string dueDate;
        public string difficulty;

        public Quest(string name, string due, string diff)
        {
            this.questName = name;
            this.dueDate = due;
            this.difficulty = diff;
        }
        public Quest()
        {

        }

        public override string ToString()
        {
            return questName + " " + dueDate + " " + difficulty;
        }
    }
}
