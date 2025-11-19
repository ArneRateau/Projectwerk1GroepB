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
        string[] nrmlData = {};
        public CreateProjectSpecifiek(string[] doorgever)
        {
            InitializeComponent();
            nrmlData = doorgever;
        }

        private void CreeerProject_Click(object sender, RoutedEventArgs e)
        {
            if (StadsOntwikkeling.IsChecked ?? true)
            {
                //geen controles added YET

                bool bam, cord, alhe, demo, archWaa, bezienWaard, uitlegBord,infoWand = false;

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

                string vergunningsStatus = VergStatus.Text;

                if (archWaarde.IsChecked ?? true)
                {
                    archWaa = true;
                }

                string openbareToegang = OpenToegan.Text;

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

            }
            if (GroeneRuimte.IsChecked ?? true)
            {
                //nog verder uit te werken
            }
            if (InnovatiefWonen.IsChecked ?? true)
            {
                //nog verder uit te werken
            }
        }
    }
}
