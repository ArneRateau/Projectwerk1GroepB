using StadOntwikkeling_BL;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Managers;
using StadOntwikkeling_DL.Repos;
using System.Windows;

namespace StadOntwikkeling_WPF
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private readonly GebruikerManager _gebruikerManager;
        private readonly ProjectManager _projectManager;
        private readonly PartnerManager _partnerManager;
        private readonly LocatieManager _locatieManager;
        private readonly ProjectPartnerManager _projectPartnerManager;

        public MainMenu(GebruikerManager gebruikerManager, ProjectManager projectManager, PartnerManager partnerManager, LocatieManager locatieManager, ProjectPartnerManager projectPartnerManager)
        {
            InitializeComponent();
            _gebruikerManager = gebruikerManager;
            _projectManager = projectManager;
            _partnerManager = partnerManager;
            _locatieManager = locatieManager;
            _projectPartnerManager = projectPartnerManager;


            ApplyPermissions();

            addGebruiker.Click += AddGebruiker_Click;
        }

        private void ApplyPermissions()
        {
            var user = AppSession.huidigeGebruiker;

            if (user == null)
            {
                Bekijk.Visibility = Visibility.Collapsed;
                MaakProject.Visibility = Visibility.Collapsed;
                addGebruiker.Visibility = Visibility.Collapsed;
                return;
            }


            if (user.IsAdmin)
            {
                // Admin kan alles zien
                Bekijk.Visibility = Visibility.Visible;
                MaakProject.Visibility = Visibility.Visible;
                addGebruiker.Visibility = Visibility.Visible;
                return;
            }

            if (user.IsPartner)
            {
                // Partner → alleen projecten bekijken
                Bekijk.Visibility = Visibility.Visible;
                MaakProject.Visibility = Visibility.Collapsed;
                addGebruiker.Visibility = Visibility.Collapsed;
                return;
            }

            // gewone gebruiker → Maak gebruiker + Bekijk projecten
            Bekijk.Visibility = Visibility.Visible;
            MaakProject.Visibility = Visibility.Visible;
            addGebruiker.Visibility = Visibility.Collapsed;
        }

        private void AddGebruiker_Click(object? sender, RoutedEventArgs e)
        {
            var window = new GebruikerToevoegen(_gebruikerManager);
            window.ShowDialog();
        }

        private void BekijkProjecten_Click(object sender, RoutedEventArgs e)
		{
            var window = new ZoekVenster(_projectManager, _partnerManager, _locatieManager, _projectPartnerManager);
            window.ShowDialog();
        }

		private void MaakProject_Click(object sender, RoutedEventArgs e)
		{
            var window = new CreataProject(_projectManager);
            window.ShowDialog();
		}

		private void MaakGebruiker_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
