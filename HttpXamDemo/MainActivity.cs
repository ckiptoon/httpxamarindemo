using System;
using System.Collections.Generic;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace HttpXamDemo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        HttpClient httpClient;
        private const string url = "https://jsonplaceholder.typicode.com/posts";
        ListView listView;
        List<Post> posts;
        PostAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;

            listView = FindViewById<ListView>(Resource.Id.listView1);

            Button button = FindViewById<Button>(Resource.Id.getTitleButton);
            button.Click += Button_Click;

            Button secondButton = FindViewById<Button>(Resource.Id.secondActivity);
            secondButton.Click += SecondButton_Click;
        }

        private void SecondButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SecondActivity));
            StartActivity(intent);         
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri(url);

            try
            {
                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    posts = JsonConvert.DeserializeObject<List<Post>>(content);

                    adapter = new PostAdapter(posts);

                    listView.Adapter = adapter;
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("ex: " +ex.Message);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

