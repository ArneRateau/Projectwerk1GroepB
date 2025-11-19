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
        private readonly IGebruikerManager _gebruikerManager;
        private readonly IProjectManager _projectManager;

        public MainMenu(IGebruikerManager gebruikerManager, IProjectManager projectManager)
        {
            InitializeComponent();
            _gebruikerManager = gebruikerManager;
            _projectManager = projectManager;

            addGebruiker.Click += AddGebruiker_Click;

        }

        private void AddGebruiker_Click(object? sender, RoutedEventArgs e)
        {
            var window = new GebruikerToevoegen(_gebruikerManager);
            window.ShowDialog();
        }

        private void BekijkProjecten_Click(object sender, RoutedEventArgs e)
		{
            var window = new ZoekVenster(_projectManager);
            window.ShowDialog();
        }

		private void MaakProject_Click(object sender, RoutedEventArgs e)
		{
            var window = new CreataProject();
            window.ShowDialog();
		}

		private void MaakGebruiker_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
