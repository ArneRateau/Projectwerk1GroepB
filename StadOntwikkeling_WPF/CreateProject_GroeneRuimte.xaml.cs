using StadOntwikkeling_BL.Interfaces;
using System.Windows;

namespace StadOntwikkeling_WPF
{
    public partial class CreateProject_GroeneRuimte : Window
    {
        private readonly string[] _data;
        private readonly IProjectManager _projectManager;

        public CreateProject_GroeneRuimte(string[] baseData, IProjectManager manager)
        {
            InitializeComponent();
            _data = baseData;
            _projectManager = manager;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            _projectManager.MaakProject(
                titel: _data[0], status: _data[1], datum: _data[2], wijk: _data[3], straat: _data[4],
                gemeente: _data[5], postcode: _data[6], huisnummer: _data[7], beschrijving: _data[8],

                // stadsontwikkeling parameters
                bam: false, cord: false, alhe: false, demo: false, vergunningsStatus: "", archWaa: false, openbareToegang: "",
                bezienWaard: false, uitlegBord: false, infoWand: false, stadsType: false,

                // groene ruimte
                oppvlak: Oppervlakte.Text,
                bioSco: BioScore.Text,
                aanWandel: AantalWandel.Text,
                speelT: Speeltuin.IsChecked == true,
                pickZone: Picknick.IsChecked == true,
                infoBord: InfoBord.IsChecked == true,
                nieuweFaciliteit: nieuweFaciliteit.Text,
                toeWand: ToerWandeling.IsChecked == true,
                bezoekScore: BezoekScore.Text,
                groeneType: true,

                // innovatief wonen parameters
                aanWoonheden: "", modulW: false, cohouW: false, nieuweWoonVorm: "", rondL: false, showW: false, innoScore: "", samErf: false, samToer: false, innovatieType: false
            );

            MessageBox.Show("Project succesvol aangemaakt!");
            this.Close();
        }
    }
}
