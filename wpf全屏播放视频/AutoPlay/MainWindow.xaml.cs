using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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

namespace AutoPlay
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaElement myPlayer = new MediaElement();
        Queue<string> vodeoList = new Queue<string>();
        public MainWindow()
        {
            InitializeComponent();
            string[] ss = Directory.GetFiles(@"D:\Videos");
            string Currenturl = "";
            foreach (string s in ss)
            {
                vodeoList.Enqueue(s);
            }
            myPlayer.Margin = new Thickness(1, 1, 1, 1);
            myPlayer.Width = ActualWidth;
            myPlayer.Height = ActualHeight;

            myPlayer.LoadedBehavior = MediaState.Manual;
            myPlayer.Source = new Uri(vodeoList.Dequeue(), UriKind.RelativeOrAbsolute);
            myPlayer.MediaEnded += MyPlayer_MediaEnded;     
            (Content as Grid).Children.Add(myPlayer);
            FullScreenHelper.GoFullscreen(this);
            myPlayer.Play();
            
        }

        private void MyPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            vodeoList.Enqueue(myPlayer.Source.AbsoluteUri);
            myPlayer.Source = new Uri(vodeoList.Dequeue(), UriKind.RelativeOrAbsolute);
            myPlayer.Play();
        }

        void myContent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (FullScreenHelper.IsFullscreen(this))
            //    FullScreenHelper.ExitFullscreen(this);
            //else
            //    FullScreenHelper.GoFullscreen(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myPlayer.Play();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myPlayer.Width = ActualWidth;
            myPlayer.Height = ActualHeight;
        }

    }
}
