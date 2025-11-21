using StadOntwikkeling_BL;
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
        private readonly GebruikerManager _gebruikerManager;
        private readonly ProjectManager _projectManager;
        private readonly PartnerManager _partnerManager;
        private readonly LocatieManager _locatieManager;
        private readonly ProjectPartnerManager _projectPartnerManager;

        public MainWindow(GebruikerManager gebruikerManager, ProjectManager projectManager, PartnerManager partnerManager, LocatieManager locatieManager, ProjectPartnerManager projectPartnerManager)
        {
            InitializeComponent();
            _gebruikerManager = gebruikerManager;
            _projectManager = projectManager;
            _partnerManager = partnerManager;
            _locatieManager = locatieManager;
            _projectPartnerManager = projectPartnerManager;
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
                AppSession.huidigeGebruiker = g;

                var mw = new MainMenu(_gebruikerManager, _projectManager, _partnerManager, _locatieManager, _projectPartnerManager);

                mw.ShowDialog();
            }
        }
    }

}