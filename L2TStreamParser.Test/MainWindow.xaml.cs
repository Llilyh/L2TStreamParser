using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using L2TStreamParser.Test.Annotations;
using LinqToTwitter;

namespace L2TStreamParser.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            this.startButton.IsEnabled = false;

            var twCtx = new TwitterContext(new SingleUserAuthorizer()
            {
                CredentialStore = new SingleUserInMemoryCredentialStore()
                {
                    ConsumerKey = this.consumerKeyBox.Text,
                    ConsumerSecret = this.consumerSecretBox.Text,
                    AccessToken = this.oauthTokenBox.Text,
                    AccessTokenSecret = this.oauthTokenSecretBox.Text
                }
            });

            var count = 0;

            var streamParser = (from strm in twCtx.Streaming where strm.Type == StreamingType.Filter && strm.Track == "test" select strm).CreateParser();
            streamParser.ReceivedResult +=
                (_sender, _args) =>
                {
                    Console.WriteLine(
                        "Result from stream : ");
                    Console.WriteLine(_args.Tweet.id_long);
                    Console.WriteLine(_args.Tweet.text);
                    Console.WriteLine(
                        _args.Tweet.Geo.latitude);
                    Console.WriteLine(
                        _args.Tweet.Geo.longitude);
                    Console.WriteLine(
                        _args.Tweet.User.name);
                    Console.WriteLine(
                        _args.Tweet.CreatedAt);
                    Console.WriteLine(
                        "----------------------");
                };

                streamParser.ParseContent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
