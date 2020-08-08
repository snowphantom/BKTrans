using BKTrans.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace BKTrans
{
    /// <summary>
    /// Interaction logic for AddLanguageControl.xaml
    /// </summary>
    public partial class AddLanguageControl : UserControl
    {
       

        public AddLanguageControl()
        {
            InitializeComponent();
            DataContext = new AddLangViewModel();
        }

        
    }
}
