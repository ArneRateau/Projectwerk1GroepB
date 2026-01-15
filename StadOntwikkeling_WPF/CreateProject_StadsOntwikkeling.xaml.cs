using StadOntwikkeling_BL.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace StadOntwikkeling_WPF
{
    public partial class CreateProject_StadsOntwikkeling : Window
    {
        private readonly string[] _data;
        private readonly IProjectManager _projectManager;

        public CreateProject_StadsOntwikkeling(string[] baseData, IProjectManager manager)
        {
            InitializeComponent();
            _data = baseData;
            _projectManager = manager;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            _projectManager.MaakProject(
                titel: _data[0],
                status: _data[1],
                datum: _data[2],
                wijk: _data[3],
                straat: _data[4],
                gemeente: _data[5],
                postcode: _data[6],
                huisnummer: _data[7],
                beschrijving: _data[8],

                bam: BAM.IsChecked == true,
                cord: Cordeel.IsChecked == true,
                alhe: Alheembouw.IsChecked == true,
                demo: Democo.IsChecked == true,
                vergunningsStatus: (VergStatus.SelectedItem as ComboBoxItem)?.Content.ToString(),
                archWaa: ArchWaarde.IsChecked == true,
                openbareToegang: (OpenToegang.SelectedItem as ComboBoxItem)?.Content.ToString(),
                bezienWaard: BezienWa.IsChecked == true,
                uitlegBord: Uitlegbord.IsChecked == true,
                infoWand: Infowandeling.IsChecked == true,
                stadsType: true,

                // maakproject verwacht nog groene ruimte en innovatief wonen parameters
                oppvlak: "", bioSco: "", aanWandel: "", speelT: false, pickZone: false, infoBord: false, nieuweFaciliteit: "", toeWand: false, bezoekScore: "", groeneType: false,

                // idem
                aanWoonheden: "", modulW: false, cohouW: false, nieuweWoonVorm: "", rondL: false, showW: false, innoScore: "", samErf: false, samToer: false, innovatieType: false
            );

            MessageBox.Show("Project succesvol aangemaakt!");
            this.Close();
        }
    }
}
