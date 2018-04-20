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
        public int questId { get; set; }
        public string questName { get; set; }
        public string dueDate { get; set; }
        public string Difficulty { get; set; }

        public Quest(string name, string due, string diff)
        {
            this.questName = name;
            this.dueDate = due;
            this.Difficulty = diff;
        }

        public Quest()
        {

        }

        public override string ToString()
        {
            return  "Quest Name: " + questName + " Due Date: " + dueDate + " Difficulty: " + Difficulty;
        }
    }
}