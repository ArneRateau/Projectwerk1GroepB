using StadOntwikkeling_BL.Interfaces;
using System.Windows;

namespace StadOntwikkeling_WPF
{
    public partial class CreateProject_InnovatiefWonen : Window
    {
        private readonly string[] _data;
        private readonly IProjectManager _projectManager;

        public CreateProject_InnovatiefWonen(string[] baseData, IProjectManager manager)
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

                // stadsontwikkeling 
                bam: false, cord: false, alhe: false, demo: false, vergunningsStatus: "", archWaa: false, openbareToegang: "",
                bezienWaard: false, uitlegBord: false, infoWand: false, stadsType: false,

               // groene 
               oppvlak: "", bioSco: "", aanWandel: "", speelT: false, pickZone: false, infoBord: false, nieuweFaciliteit: "", toeWand: false, bezoekScore: "", groeneType: false,


                // innovatief wonen
                aanWoonheden: AantalWoon.Text,
                modulW: Modulair.IsChecked == true,
                cohouW: Cohousing.IsChecked == true,
                nieuweWoonVorm: ExtraWoonvorm.Text,
                rondL: Rondleiding.IsChecked == true,
                showW: Showwoning.IsChecked == true,
                innoScore: InnovScore.Text,
                samErf: SamenErfgoed.IsChecked == true,
                samToer: SamenToerisme.IsChecked == true,
                innovatieType: true
            );

            MessageBox.Show("Project succesvol aangemaakt!");
            this.Close();
        }
    }
}
