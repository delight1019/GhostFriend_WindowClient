using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GhostFriendClient.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _player1Name;
        public string Player1Name {
            get { return _player1Name; }
            set
            {
                _player1Name = value;
                NotifyPropertyChanged("Player1Name");
            }
        }

        private string _player2Name;
        public string Player2Name
        {
            get { return _player2Name; }
            set
            {
                _player2Name = value;
                NotifyPropertyChanged("Player2Name");
            }
        }

        private string _player3Name;
        public string Player3Name
        {
            get { return _player3Name; }
            set
            {
                _player3Name = value;
                NotifyPropertyChanged("Player3Name");
            }
        }

        private string _player4Name;
        public string Player4Name
        {
            get { return _player4Name; }
            set
            {
                _player4Name = value;
                NotifyPropertyChanged("Player4Name");
            }
        }

        private string _player5Name;
        public string Player5Name
        {
            get { return _player5Name; }
            set
            {
                _player5Name = value;
                NotifyPropertyChanged("Player5Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }        
    }
}
