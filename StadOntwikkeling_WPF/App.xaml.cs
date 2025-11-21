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
        private PartnerManager _partnerManager;
        private LocatieManager _locatieManager;
        private ProjectPartnerManager _projectPartnerManager;
        //private string _connectionString = "Data Source=MRROBOT\\SQLEXPRESS;Initial Catalog = GentProjecten; Integrated Security = True; Encrypt=True;Trust Server Certificate=True";
        private string _connectionString = "Data Source=localhost;Initial Catalog=GentProjecten;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var gebruikerRepo = new GebruikerRepository(_connectionString);
            var ProjectRepo = new ProjectRepository(_connectionString);
            var PartnerRepo = new PartnerRepository(_connectionString); 
            var locatieRepo = new LocatieRepository(_connectionString);
            var projectPartnerRepo = new ProjectPartnerRepository(_connectionString);
            _gebruikerManager = new GebruikerManager(gebruikerRepo);
            _projectManager = new ProjectManager(ProjectRepo);
            _partnerManager = new PartnerManager(PartnerRepo);
            _locatieManager = new LocatieManager(locatieRepo);
            _projectPartnerManager = new ProjectPartnerManager(projectPartnerRepo);



            var loginWindow = new MainWindow(_gebruikerManager, _projectManager, _partnerManager, _locatieManager, _projectPartnerManager);
            loginWindow.ShowDialog();
            //var mainMenu = new MainMenu(_gebruikerManager, _projectManager, _partnerManager, _locatieManager, _projectPartnerManager);

        }
    }

}
