using Microsoft.IdentityModel.Tokens;
using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Interfaces;
using StadOntwikkeling_BL.Managers;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Nrbf;
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
	/// Interaction logic for ProjectWindow.xaml
	/// </summary>
	public partial class ProjectWindow : Window
	{
		//public List<Partner> Partners { get; private set; } = new();
		private readonly ProjectManager _projectManager;
		private readonly PartnerManager _partnerManager;
		private readonly LocatieManager _locatieManager;
		private readonly ProjectPartnerManager _projectPartnerManager;
		private Project p;
		private ObservableCollection<Partner> _currentPartners = new();
		private ObservableCollection<Partner> _beschikbaarPartners = new();
		private ObservableCollection<ProjectPartner> _projectPartner = new();

		public ProjectWindow(Project project, ProjectManager projectManager, PartnerManager partnerManager, LocatieManager locatieManager, ProjectPartnerManager projectPartnerManager)
		{
			InitializeComponent();
			p = project;
			_projectManager = projectManager;
			_partnerManager = partnerManager;
			_locatieManager = locatieManager;
			_projectPartnerManager = projectPartnerManager;

			_currentPartners = new ObservableCollection<Partner>(_partnerManager.GetPartnersByProjectId(p.Id));
			_beschikbaarPartners = new ObservableCollection<Partner>(_partnerManager.GetAllPartners().Where(ap => _currentPartners.All(cp => cp.Id != ap.Id)).OrderBy(ap => ap.Naam));


			FillCurrentValues();
			FillNewValues();
			LoadPartners();
		}

		
		private void FillCurrentValues()
		{
			TextBlock_Id.Text = p.Id.ToString();
			TextBlock_Titel.Text = p.Titel;
			TextBlock_Datum.Text = p.StartDatum.ToString("dd/MM/yyyy");
			TextBlock_Status.Text = p.Status.ToString();
			TextBlock_Beschrijving.Text = p.Beschrijving;

			TextBlock_Locatie_Straat.Text = p.Locatie.Straat;
			TextBlock_Locatie_Huisnummer.Text = p.Locatie.Huisnummer;
			TextBlock_Locatie_Postcode.Text = p.Locatie.Postcode;
			TextBlock_Locatie_Gemeente.Text = p.Locatie.Gemeente;
			TextBlock_Locatie_Wijk.Text = p.Locatie.Wijk;
		}

	   
		private void FillNewValues()
		{
			TextBox_New_Id.Text = p.Id.ToString();
			TextBox_New_Titel.Text = p.Titel;
			DatePicker_New_Datum.SelectedDate = p.StartDatum;

			ComboBox_New_Status.SelectedItem =
				ComboBox_New_Status.Items
					.Cast<ComboBoxItem>()
					.FirstOrDefault(i => i.Content.ToString() == p.Status.ToString());

			TextBox_New_Beschrijving.Text = p.Beschrijving;

			TextBox_New_Straat.Text = p.Locatie.Straat;
			TextBox_New_Huisnummer.Text = p.Locatie.Huisnummer;
			TextBox_New_Postcode.Text = p.Locatie.Postcode;
			TextBox_New_Gemeente.Text = p.Locatie.Gemeente;
			TextBox_New_Wijk.Text = p.Locatie.Wijk;
		}


		private void LoadPartners()
		{
			//// Clear both lists
			//ListBox_Partners_Current.Items.Clear();
			//ListBox_Partners_All.Items.Clear();

			
			//var currentPartners = _partnerManager.GetPartnersByProjectId(p.Id);

			//foreach (var partner in _currentPartners)
			//{
			//	ListBox_Partners_Current.Items.Add(
			//		$"{partner.Naam} ({partner.Email})"
			//	);
			//}
			ListBox_Partners_All.ItemsSource = _beschikbaarPartners;
			ListBox_Partners_Current.ItemsSource = _currentPartners;

			//var allPartners = _partnerManager.GetAllPartners();


			//// filter by excluding the ones already in the project
			//var remaining = allPartners
			//				.Where(ap => _currentPartners.All(cp => cp.Id != ap.Id))
			//				.OrderBy(ap => ap.Naam)
			//				.ToList();

			//foreach (var partner in _beschikbaarPartners)
			//{
			//	ListBox_Partners_All.Items.Add(
			//		$"{partner.Naam} ({partner.Email})"
			//	);
			//}
		}


		private void Button_Opslaan(object sender, RoutedEventArgs e)
		{
			try
			{
				// Titel
				if (!string.IsNullOrWhiteSpace(TextBox_New_Titel.Text))
					p.Titel = TextBox_New_Titel.Text;

				// Datum
				if (DatePicker_New_Datum.SelectedDate.HasValue)
					p.StartDatum = DatePicker_New_Datum.SelectedDate.Value;

				// Status
				if (ComboBox_New_Status.SelectedItem is ComboBoxItem item)
					p.Status = Enum.Parse<Status>(item.Content.ToString());

				// Beschrijving
				if (!string.IsNullOrWhiteSpace(TextBox_New_Beschrijving.Text))
					p.Beschrijving = TextBox_New_Beschrijving.Text;

				// Locatie
				p.Locatie.Straat = TextBox_New_Straat.Text;
				p.Locatie.Huisnummer = TextBox_New_Huisnummer.Text;
				p.Locatie.Postcode = TextBox_New_Postcode.Text;
				p.Locatie.Gemeente = TextBox_New_Gemeente.Text;
				p.Locatie.Wijk = TextBox_New_Wijk.Text;

				// Save to DB
				// Save project
				_projectManager.updateProject(p);

				// Save locatie
				_locatieManager.updateLocatie(p.Locatie);

				//Partners = _currentPartners.ToList();

				MessageBox.Show("Project opgeslagen.");
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Fout: {ex.Message}");
			}
		}

		private void Button_PartnerKoppelen(object sender, RoutedEventArgs e)
		{
			PartnerPopup popup = new PartnerPopup(p, _beschikbaarPartners);
			if (popup.ShowDialog() == true && popup.GemaakteProjectPartner != null)
			{
				var pp = popup.GemaakteProjectPartner;

				// Sla op via BL
				_projectPartnerManager.KoppelPartnerAanProject(p, pp.Partner, pp.Rol);

				// Update de ObservableCollections
				_currentPartners.Add(pp.Partner);
				_beschikbaarPartners.Remove(pp.Partner);
			}

			// After closing the popup, reload the partners
			p = _projectManager.GetProjectById(p.Id);
			LoadPartners();
		}

		private void ListBox_Partners_All_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (ListBox_Partners_All.SelectedItem == null)
				return;

			// The ListBox items look like: "Naam (email)"
			string selected = ListBox_Partners_All.SelectedItem.ToString();

			// Extract the partner's name (before first '(' )
			string partnerNaam = selected.Split('(')[0].Trim();

			// manier om juiste partner te vinden zodat role juist kan toegewezen worden
			//Partner partner = _partnerManager.GetPartnerByName(partnerNaam);

			//if (partner == null)
			//{
			//    MessageBox.Show("Partner kon niet geladen worden.");
			//    return;
			//}

			// Open the partner window
			PartnerPopup partnerPopup = new PartnerPopup(p, (Partner)ListBox_Partners_All.SelectedItem, _beschikbaarPartners);//hier kan partner meegegeven worden als parameter
			partnerPopup.ShowDialog();
		}

	}
}
