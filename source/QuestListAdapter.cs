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
    class QuestListAdapter : BaseAdapter<Quest>
    {
        Quest[] myQuests;
        Activity context;

        //Constructor
        public QuestListAdapter(Activity context, Quest[] quests) : base()
        {
            this.myQuests = quests;
            this.context = context;
        }

        //Override for GetItemID implementation
        public override long GetItemId(int position)
        {
            return position;
        }

        //Override for position implementation
        public override Quest this[int position]
        {
            get
            {
                return myQuests[position];
            }
        }

        //Override for Count implementation
        public override int Count
        {
            get
            {
                return myQuests.Length;
            }
        }

        //Override GetView implementation
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //Sets up re-useable views
            View view = convertView;
            if(view == null)
            {
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = myQuests[position].ToString();
            return view;
        }
    }
}
