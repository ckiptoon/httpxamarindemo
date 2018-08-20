using Android.OS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpXamDemo
{
    public class DataService
    {
        HttpClient client;
        public List<Post> Posts { get; set; }
        private const string postUrl = "https://jsonplaceholder.typicode.com/posts";

        public DataService()
        {
            client = new HttpClient { MaxResponseContentBufferSize = 256000 };

        }

        public async Task<List<Post>> GetPosts()
        {
            Posts = new List<Post>();
            Uri uri = new Uri(postUrl);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //Bundle bundle = new Bundle();
                    //bundle.PutString("myPosts", content);

                    Posts = JsonConvert.DeserializeObject<List<Post>>(content);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ex: " + ex);
                throw new Exception();
            }

            return Posts;
        }
    }
}