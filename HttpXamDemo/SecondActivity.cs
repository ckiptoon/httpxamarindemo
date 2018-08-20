using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using V4Fragment = Android.Support.V4.App.Fragment;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace HttpXamDemo
{
    [Activity(Label = "SecondActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class SecondActivity : AppCompatActivity
    {
        List<Post> posts;
        DataService dataService;
        string postsString;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.second_activity);
            // Create your application here

            dataService = new DataService();
            LoadPosts(dataService);

            V7Toolbar toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar2);
            SetSupportActionBar(toolbar);

            Button button = FindViewById<Button>(Resource.Id.secondButton);

            button.Click += Button_Click;
        }

        private async void LoadPosts(DataService dataService)
        {
            posts = await dataService.GetPosts();
            postsString = JsonConvert.SerializeObject(posts);
            Bundle bundle = new Bundle();
            bundle.PutString("myPosts", postsString);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            V4Fragment fragment = Fragment1.NewInstance(postsString);
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.frame_container, fragment)
                .Commit();
        }
    }
}