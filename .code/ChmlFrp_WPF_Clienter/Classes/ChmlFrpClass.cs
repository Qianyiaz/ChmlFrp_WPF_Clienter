using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ChmlFrp_WPF_Clienter.Classes
{
    internal class ModelClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Userpassword;
        public string Userpassword
        {
            get { return _Userpassword; }
            set
            {
                if (_Userpassword != value)
                {
                    _Userpassword = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _Username;

        public string Username
        {
            get { return _Username; }
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
