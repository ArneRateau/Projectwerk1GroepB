using StadOntwikkeling_BL.Interfaces;
using System;
using System.Collections.Generic;
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


namespace StadOntwikkeling_WPF
{
    /// <summary>
    /// Interaction logic for CreateProjectSpecifiek.xaml
    /// </summary>
    public partial class CreateProjectSpecifiek : Window
    {
        private string[] _nrmlData = {};
        private IProjectManager _projectManager;

        public CreateProjectSpecifiek(string[] doorgever)
        {
        }

        public CreateProjectSpecifiek(string[] doorgever,IProjectManager projectManager)
        {
            InitializeComponent();
            _nrmlData = doorgever;
            _projectManager = projectManager;
        }

        private void CreeerProject_Click(object sender, RoutedEventArgs e)
        {
            bool stadsType = false, groeneType = false, innovatieType = false;
            bool bam = false, cord = false, alhe = false, demo = false, archWaa = false, bezienWaard = false, uitlegBord, infoWand = false;
            string vergunningsStatus, openbareToegang;
            bool speelT = false, pickZone = false, infoBord = false, toeWand = false;
            string oppvlak,bioSco,aanWandel, nieuweFaciliteit, bezoekScore;
            bool modulW = false, cohouW = false, rondL = false, showW = false, samErf = false, samToer = false;
            string aanWoonheden, nieuweWoonVorm, innoScore;
            if (StadsOntwikkeling.IsChecked ?? true)
            {
                //geen controles added YET voor textvelden


                if(BAM.IsChecked ?? true) {
                    bam = true;
                }
                if(Cordeel.IsChecked ?? true) {
                    cord = true;
                }
                if(Alheembouw.IsChecked ?? true) {
                    alhe = true;
                }
                if(Democo.IsChecked ?? true) {
                    demo = true;
                }

                vergunningsStatus = VergStatus.Text;

                if (archWaarde.IsChecked ?? true)
                {
                    archWaa = true;
                }

                openbareToegang = OpenToegan.Text;

                if (BezWaard.IsChecked ?? true) {
                    bezienWaard = true;
                }

                if (UitBord.IsChecked ?? true) { 
                    uitlegBord = true;
                }

                if (InfoWand.IsChecked ?? true)
                {
                    infoWand = true;
                }

                stadsType = true;
            }
            if (GroeneRuimte.IsChecked ?? true)
            {
                //TODO add controles textvelden
                
                oppvlak=Oppervlakte.Text;

                bioSco=BioScore.Text;

                aanWandel=AantWandel.Text;

                if (spel.IsChecked ?? true){
                    speelT=true;
                }
                if (pick.IsChecked ?? true)
                {
                    pickZone = true;
                }
                if (info.IsChecked ?? true)
                {
                    infoBord = true;
                }

                nieuweFaciliteit = Faciliteit.Text; //kan & mag leeg  zijn

                if (ToerWand.IsChecked ?? true)
                {
                    toeWand = true;
                }

                bezoekScore = BezoeScore.Text;

                groeneType = true;
            }
            if (InnovatiefWonen.IsChecked ?? true)
            {
                //TODO add controles textvelden
               

                aanWoonheden=AantWoon.Text;

                if (Modul.IsChecked ?? true)
                {
                    modulW=true;
                }

                if (Cohou.IsChecked ?? true)
                { 
                    cohouW=true; 
                }

                nieuweWoonVorm = Woonvorm.Text; //kan & mag leeg zijn

                if (Rondleid.IsChecked ?? true)
                {
                    rondL = true;
                }

                if (ShowWoni.IsChecked ?? true)
                {
                    showW=true;
                }

                innoScore = InnoSco.Text;

                if (SamErf.IsChecked ?? true)
                {
                    samErf = true;
                }

                if (SamToe.IsChecked ?? true)
                {
                    samToer = true;
                }
                innovatieType = true;
            }
            if (stadsType == false & groeneType == false & innovatieType == false)
            {
                MessageBox.Show("Kies minstens 1 type project");
            }

            _projectManager.MaakProject();


        }
    }
}
