using GhostFriendClient.ViewModel;
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
            this.DataContext = new MainWindowViewModel();
        }
    }
}
