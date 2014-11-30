using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TweetSharp;

namespace Twitter_reti_complesse
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TwitterService service;
        OAuthRequestToken requestToken;
        public MainWindow()
        {
            InitializeComponent();
            // Pass your credentials to the service
            service = new TwitterService("WZ5hMkmyXROcuvBEPsadEGoSp", "x18KRjz5chubYY3bXQPP3cCs9xNzr6NK5482Ewpw9fmqlszu3L");
            service.AuthenticateWith("112144477-xM9Kq0ACK4exXqpbsLS2y0pIaV6HYU82w6UJTLF4", "3hm0gyO7MXuIAqIlVjPsI9cHri27t47y0q2cP7wTrcQVH");
            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());
            loadUserIcon(user.ProfileImageUrlHttps);

            // Step 1 - Retrieve an OAuth Request Token
           // requestToken = service.GetRequestToken();
            
            // Step 2 - Redirect to the OAuth Authorization URL
           // Uri uri = service.GetAuthorizationUri(requestToken);
            //Process.Start(uri.ToString());

            // Step 3 - Exchange the Request Token for an Access Token

            //IEnumerable<TwitterStatus> mentions = service.ListTweetsMentioningMe()
            
            
            //service.BeginSearch();
        }

        private void loadUserIcon(string p)
        {
            try
            {
                var image = new BitmapImage();
                int BytesToRead = 100;

                WebRequest request = WebRequest.Create(new Uri("p", UriKind.Absolute));
                request.Timeout = -1;
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                BinaryReader reader = new BinaryReader(responseStream);
                MemoryStream memoryStream = new MemoryStream();

                byte[] bytebuffer = new byte[BytesToRead];
                int bytesRead = reader.Read(bytebuffer, 0, BytesToRead);

                while (bytesRead > 0)
                {
                    memoryStream.Write(bytebuffer, 0, bytesRead);
                    bytesRead = reader.Read(bytebuffer, 0, BytesToRead);
                }

                image.BeginInit();
                memoryStream.Seek(0, SeekOrigin.Begin);

                image.StreamSource = memoryStream;
                image.EndInit();

                userImage.Source = image;
            }
            catch (UriFormatException e)
            {
                
                ;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string verifier = codiceVerifica.Text; // <-- This is input into your application by your user
            //OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);

            // Step 4 - User authenticates using the Access Token
           // service.AuthenticateWith(access.Token, access.TokenSecret);

            /*
            IAsyncResult result = service.BeginListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());
            IEnumerable<TwitterStatus> tweets = service.EndListTweetsOnHomeTimeline(result);

            foreach (var tweet in tweets)
            {
                 provatxtbox.Text+=tweet.User.ScreenName +": "+ tweet.Text+"\n";
            }
            */

            SearchOptions options = new SearchOptions();
            options.Q="#moncler";
            options.Lang = "fr";
            options.Count = 100;
            IAsyncResult result = service.BeginSearch(options);
            TwitterSearchResult tweets= service.EndSearch(result);

          //  if(tweets.Statuses.Count<TwitterStatus>() > 0)
            foreach (var tweet in tweets.Statuses)
            {
              // if(tweet.Language=="it")
                 provatxtbox.Text+=tweet.Language+ "\n";
            }


        }
    }
}