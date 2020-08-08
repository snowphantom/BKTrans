using System.ComponentModel;

namespace BKTrans.Core
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected static event PropertyChangedEventHandler GlobalPropertyChanged = delegate { };
        public static void OnGlobalPropertyChanged(string propertyName)
        {
            GlobalPropertyChanged(typeof(object), new PropertyChangedEventArgs(propertyName));
        }
    }
}
