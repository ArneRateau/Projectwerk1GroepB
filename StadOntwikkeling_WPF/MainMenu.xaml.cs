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

        public MainMenu(IGebruikerManager gebruikerManager)
        {
            InitializeComponent();
            _gebruikerManager = gebruikerManager;

            addGebruiker.Click += AddGebruiker_Click;
        }

        private void AddGebruiker_Click(object? sender, RoutedEventArgs e)
        {
            var window = new GebruikerToevoegen(_gebruikerManager);
            window.ShowDialog();
        }

        private void BekijkProjecten_Click(object sender, RoutedEventArgs e)
		{
            var window = new ZoekVenster();
            window.ShowDialog();
        }

		private void MaakProject_Click(object sender, RoutedEventArgs e)
		{

		}

		private void MaakGebruiker_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
