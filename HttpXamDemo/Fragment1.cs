using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using V4Fragment = Android.Support.V4.App.Fragment;

namespace HttpXamDemo
{
    public class Fragment1 : V4Fragment
    {
        ListView listView;
        FragmentPostAdapter adapter;
        List<Post> posts;
        private const string url = "https://jsonplaceholder.typicode.com/posts";

        public static Fragment1 NewInstance(string posts)
        {
            Bundle bundle = new Bundle();
            bundle.PutString("incomingPosts", posts);
            return new Fragment1 { Arguments = bundle };
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            posts = new List<Post>();
            var postsString = Arguments.GetString("incomingPosts");
            posts = JsonConvert.DeserializeObject<List<Post>>(postsString);

            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.fragment1, container, false);
            listView = view.FindViewById<ListView>(Resource.Id.fragmentListView);

            if (posts == null)
            {
                posts = new List<Post>
                {
                    new Post { Title = "No Post", Body = "No Body", Id = 1, UserId = 1 }
                };
            }
               

            adapter = new FragmentPostAdapter(Application.Context, posts);

            listView.Adapter = adapter;

            return view;
        }
    }
}