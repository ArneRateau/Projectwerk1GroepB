using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Models;


namespace StadOntwikkeling_WPF
{
    public partial class PartnerPopup : Window
	{
		//private readonly IPartnerManager _partnerManager;
        private Partner _geselecteerdePartner;
        private Project _project;
        private string _partnerRol;
        public ProjectPartner GemaakteProjectPartner { get; private set; } //get;

        public PartnerPopup( Project project, ObservableCollection<Partner> beschikbarePartners)
        {
            InitializeComponent();
            //_partnerManager = partnerManager;
            _project = project;
            CmbPartners.ItemsSource = beschikbarePartners;
        }

		public PartnerPopup(Project project, Partner partner, ObservableCollection<Partner> beschikbarePartners)
		{
			InitializeComponent();
			//_partnerManager = partnerManager;
			CmbPartners.ItemsSource = beschikbarePartners;
            CmbPartners.DisplayMemberPath = partner.ToString() ;
            _project = project;
            _geselecteerdePartner = partner;
		}

		private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

			if (CmbPartners.SelectedItem == null)
			{
				MessageBox.Show("Selecteer een partner.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (string.IsNullOrWhiteSpace(TxtRol.Text))
            {
                MessageBox.Show("Rol is verplicht.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
			_geselecteerdePartner = (Partner)CmbPartners.SelectedItem;
			_partnerRol = TxtRol.Text.Trim();

            GemaakteProjectPartner = new ProjectPartner(_geselecteerdePartner, _project, _partnerRol);

            DialogResult = true;   // belangrijk zodat ShowDialog() == true wordt
            Close();
        }

        private void BtnAnnuleer_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
