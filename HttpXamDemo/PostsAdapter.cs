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
    public class PostAdapter : BaseAdapter<Post>
    {
        List<Post> posts;

        public PostAdapter(List<Post> posts)
        {
            this.posts = posts;
        }

        public override Post this[int position] => posts[position];

        public override int Count => posts.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            PostViewHolder holder = null;

            if(view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item, parent, false);
                holder = new PostViewHolder();
                holder.Title = view.FindViewById<TextView>(Resource.Id.titleTextView);

                view.Tag = holder;
            }
            else
            {
                holder = view.Tag as PostViewHolder;
            }

            holder.Title.Text = posts[position].Title;

            return view;
        }
    }
}