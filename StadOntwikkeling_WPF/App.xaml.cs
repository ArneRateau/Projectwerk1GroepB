using StadOntwikkeling_BL.Managers;
using StadOntwikkeling_DL.Repos;
using System.Configuration;
using System.Data;
using System.Windows;

namespace StadOntwikkeling_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private GebruikerManager _gebruikerManager;
        private ProjectManager _projectManager;
        private string _connectionString = "Data Source=MRROBOT\\SQLEXPRESS;Initial Catalog = GentProjecten; Integrated Security = True; Encrypt=True;Trust Server Certificate=True";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var gebruikerRepo = new GebruikerRepository(_connectionString);
            var ProjectRepo = new ProjectRepository(_connectionString);
            _gebruikerManager = new GebruikerManager(gebruikerRepo);
            _projectManager = new ProjectManager(ProjectRepo);


            var mainMenu = new MainMenu(_gebruikerManager, _projectManager);
            mainMenu.Show();
        }
    }

}
