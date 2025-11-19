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
        private readonly string _connectionstring;
        public MainMenu(string connectionstring)
        {
            InitializeComponent();
            _connectionstring = connectionstring;
            // repo
            var gebruikerRepo = new GebruikerRepository(connectionstring);
            // manager
            _gebruikerManager = new GebruikerManager(gebruikerRepo);

            addLogin.Click += AddLogin_Click;
        }

        private void AddLogin_Click(object? sender, RoutedEventArgs e)
        {
            GebruikerToevoegen gebruikerToevoegenWindow = new GebruikerToevoegen(_gebruikerManager);
            gebruikerToevoegenWindow.ShowDialog();
        }

		private void BekijkProjecten_Click(object sender, RoutedEventArgs e)
		{

		}

		private void MaakProject_Click(object sender, RoutedEventArgs e)
		{

		}

		private void MaakGebruiker_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
