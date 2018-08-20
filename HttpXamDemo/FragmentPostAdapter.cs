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

namespace HttpXamDemo
{
    public class FragmentPostAdapter : BaseAdapter<Post>
    {
        List<Post> posts;
        Context context;

        public FragmentPostAdapter(Context context, List<Post> posts)
        {
            this.context = context;
            this.posts = posts;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Post this[int position] => posts[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            FragmentPostAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as FragmentPostAdapterViewHolder;

            if (holder == null)
            {
                holder = new FragmentPostAdapterViewHolder();
                //replace with your item and your holder items
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.title_item, parent, false);
                holder.Title = view.FindViewById<TextView>(Resource.Id.titleItemTextView);
                view.Tag = holder;
            }


            //fill in your items
            holder.Title.Text = posts[position].Title;

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return posts.Count;
            }
        }
    }

    class FragmentPostAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView Title { get; set; }
    }
}