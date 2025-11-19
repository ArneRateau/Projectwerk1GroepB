using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Managers;
using StadOntwikkeling_DL.Repos;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StadOntwikkeling_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IGebruikerManager _gebruikerManager;

        public MainWindow(IGebruikerManager gebruikerManager)
        {
            InitializeComponent();
            _gebruikerManager = gebruikerManager;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = Email_TextBox.Text;
            var g = _gebruikerManager.ZoekGebruikerMetEmail(email);

            if (g is null)
            {
                MessageBox.Show($"email: {email} bestaat niet jij bent niet echt!!");
                Email_TextBox.Clear();
            }
            else
            {
                var mw = new MainMenu(_gebruikerManager);
                mw.ShowDialog();
            }
        }
    }

}