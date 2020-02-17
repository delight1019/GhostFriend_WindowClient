using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace GhostFriendClient
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClickJoinGame(object sender, RoutedEventArgs e)
        {            
            SocketClient.Instance.StartConnection();
            GameControl.Join(PlayerName.Text);
        }
    }
}
