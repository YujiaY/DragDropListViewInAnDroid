using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Views;
using Android.Graphics;
using Android.Util;
using System;
using Android.Graphics.Drawables;

namespace DragDropListviewDroid
{
    [Activity(Label = "DragDropListviewDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        List<string> items;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);


            Button LoggingBt = FindViewById<Button>(Resource.Id.logbutton);

            string tag = "myapp";

            Log.Info(tag, "Start OnCreate");
            int count = 0;

           Log.Info(tag, "Setting up button event handler. The variable count is " + count);

            LoggingBt.Click += ((o, e) =>
            {
            LoggingBt.Text = string.Format("{0} clicks!", ++count);
            Log.Info(tag, "Now the counter is " + count);
            });

            var list = FindViewById<DraggableListView>(Resource.Id.draggableListView1);        

            items = new List<string> {
                "Vegetables",
                "Fruits",
                "Flower Buds",
                "Legumes",
                "Trees",
                "Grass",
                "Petals",
                "Shrubs",
            };
            list.Adapter = new DraggableListAdapter(this, items);
        }

        public class DraggableListAdapter : BaseAdapter, IDraggableListAdapter
        {
            public List<string> Items { get; set; }


            public int mMobileCellPosition { get; set; }

            Activity context;

            public DraggableListAdapter(Activity context, List<string> items) : base()
            {
                Items = items;
                this.context = context;
                mMobileCellPosition = int.MinValue;
            }

            public override Java.Lang.Object GetItem(int position)
            {
                return Items[position];
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                //View setup
                View cell = convertView;
                if (cell == null)
                {
                    cell = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
                    cell.SetMinimumHeight(100);


                    //cell.SetBackgroundDrawable(Drawable d);
                    cell.SetBackgroundResource(Resource.Drawable.Icon);
                    cell.SetPaddingRelative(32 , 10, 15, 10);
                    cell.SetBackgroundColor(Color.Aquamarine);
                }

                var text = cell.FindViewById<TextView>(Android.Resource.Id.Text1);
                if (text != null)
                {
                    //text.Text = position.ToString();
                    text.Text = Items[position]; // I changed this line to show the item values in the list
                }

                cell.Visibility = mMobileCellPosition == position ? ViewStates.Invisible : ViewStates.Visible;
                cell.TranslationY = 0;

                return cell;
            }

            public override int Count
            {
                get
                {
                    return Items.Count;
                }
            }

            public void SwapItems(int indexOne, int indexTwo)
            {
                var oldValue = Items[indexOne];
                Items[indexOne] = Items[indexTwo];
                Items[indexTwo] = oldValue;
                mMobileCellPosition = indexTwo;
                NotifyDataSetChanged();
            }

        }

    }
}

