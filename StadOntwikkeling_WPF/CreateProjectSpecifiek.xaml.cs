using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace StadOntwikkeling_WPF
{
    /// <summary>
    /// Interaction logic for CreateProjectSpecifiek.xaml
    /// </summary>
    public partial class CreateProjectSpecifiek : Window
    {
        private string[] _nrmlData = {};
        private IProjectManager _projectManager;
       

        public CreateProjectSpecifiek(string[] doorgever,IProjectManager projectManager)
        {
            InitializeComponent();
            _nrmlData = doorgever;
            _projectManager = projectManager;
        }

        private void CreeerProject_Click(object sender, RoutedEventArgs e)
        {
            bool stadsType = false, groeneType = false, innovatieType = false;
            bool bam = false, cord = false, alhe = false, demo = false, archWaa = false, bezienWaard = false, uitlegBord = false, infoWand = false;
            string vergunningsStatus="", openbareToegang="";
            bool speelT = false, pickZone = false, infoBord = false, toeWand = false;
            string oppervlakte,bioSco,aanWandel, nieuweFaciliteit, bezoekScore;
            bool modulW = false, cohouW = false, rondL = false, showW = false, samErf = false, samToer = false;
            string aanWoonheden, nieuweWoonVorm, innoScore;

            // Stadsontwikkeling
            if (StadsOntwikkeling.IsChecked ?? true)
            {
				// Controller checks
				bam = BAM.IsChecked == true;
				cord = Cordeel.IsChecked == true;
				alhe = Alheembouw.IsChecked == true;
				demo = Democo.IsChecked == true;

				vergunningsStatus = VergStatus.Text?.Trim() ?? "";
				openbareToegang = OpenToegan.Text?.Trim() ?? "";

				archWaa = archWaarde.IsChecked == true;
				bezienWaard = BezWaard.IsChecked == true;
				uitlegBord = UitBord.IsChecked == true;
				infoWand = InfoWand.IsChecked == true;

				// Dit is een verplicht veld
				if (string.IsNullOrWhiteSpace(vergunningsStatus))
				{
					MessageBox.Show("Vul een vergunningsstatus in.");
					return;
				}

				stadsType = true;
            }

            // Groenruimte
            if (GroeneRuimte.IsChecked ?? true)
            {
				//TODO add controles textvelden
				oppervlakte = Oppervlakte.Text?.Trim() ?? "";
				bioSco = BioScore.Text?.Trim() ?? "";
				aanWandel = AantWandel.Text?.Trim() ?? "";
				bezoekScore = BezoeScore.Text?.Trim() ?? "";
				nieuweFaciliteit = Faciliteit.Text?.Trim() ?? ""; // mag leeg zijn

				// Verplichte velden
				if (string.IsNullOrWhiteSpace(oppervlakte))
				{
					MessageBox.Show("Oppervlakte is verplicht.");
					return;
				}

				if (string.IsNullOrWhiteSpace(bioSco))
				{
					MessageBox.Show("Bio-score is verplicht.");
					return;
				}

				if (string.IsNullOrWhiteSpace(aanWandel))
				{
					MessageBox.Show("Aantal wandelroutes is verplicht.");
					return;
				}

				if (string.IsNullOrWhiteSpace(bezoekScore))
				{
					MessageBox.Show("Bezoekerscore is verplicht.");
					return;
				}


				speelT = spel.IsChecked == true;
				pickZone = pick.IsChecked == true;
				infoBord = info.IsChecked == true;
				toeWand = ToerWand.IsChecked == true;

				groeneType = true;
			}

            // InnovatiefWonen
            if (InnovatiefWonen.IsChecked ?? true)
            {
				//TODO add controles textvelden
				aanWoonheden = AantWoon.Text?.Trim() ?? "";
				nieuweWoonVorm = Woonvorm.Text?.Trim() ?? ""; // mag leeg zijn
				innoScore = InnoSco.Text?.Trim() ?? "";

				// verplichte velden
				if (string.IsNullOrWhiteSpace(aanWoonheden))
				{
					MessageBox.Show("Aantal woonheden is verplicht.");
					return;
				}

				if (string.IsNullOrWhiteSpace(innoScore))
				{
					MessageBox.Show("Innovatiescore is verplicht.");
					return;
				}

				modulW = Modul.IsChecked == true;
				cohouW = Cohou.IsChecked == true;
				rondL = Rondleid.IsChecked == true;
				showW = ShowWoni.IsChecked == true;
				samErf = SamErf.IsChecked == true;
				samToer = SamToe.IsChecked == true;

				innovatieType = true;
			}

			// Als de user geen enkele type heeft gekozen
            if (!stadsType & !groeneType & !innovatieType)
            {
                MessageBox.Show("Kies minstens 1 type project");
            }

			// Project aanmaken
			string titel = _nrmlData[0];
			string status = _nrmlData[1];
			string datum = _nrmlData[2];
			string wijk = _nrmlData[3];
			string straat = _nrmlData[4];
			string gemeente = _nrmlData[5];
			string postcode = _nrmlData[6];
			string huisnummer = _nrmlData[7];
			string beschrijving = _nrmlData[8];

			_projectManager.MaakProject(
				titel, status, datum, wijk, straat, gemeente, postcode, huisnummer, beschrijving,
				bam, cord, alhe, demo, vergunningsStatus, archWaa, openbareToegang, bezienWaard, uitlegBord, infoWand, stadsType
			);

			MessageBox.Show("Project succesvol aangemaakt!");
			Close();
		}
    }
}
