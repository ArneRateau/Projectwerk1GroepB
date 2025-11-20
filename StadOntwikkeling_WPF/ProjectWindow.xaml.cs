using Microsoft.IdentityModel.Tokens;
using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Managers;
using StadOntwikkeling_BL.Models;
using StadOntwikkeling_WPF.Model;
using System;
using System.Collections.Generic;
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
		private readonly ProjectManager _projectManager;
		private ProjectUI p;
		public ProjectWindow(ProjectUI project, ProjectManager projectManager)
		{
			InitializeComponent();
			p = project;
			_projectManager = projectManager;

			// vull de textblocks in met de geselecteerde project van de vorige window
			VulTextBlocksIn();

			// Toon alle partners die gelinkt zijn aan deze project
			IEnumerable<string> partners = p.Projecten.Select(p => $"Naam: {p.Partner.Naam}, Rol: {p.Rol}");
			ListBox_Partners.Items.Add(partners);
		}

		private void VulTextBlocksIn()
		{
			TextBlock_Id.Text = p.Id.ToString();
			TextBlock_Titel.Text = p.Titel;
			TextBlock_Datum.Text = p.StartDatum.ToString();
			TextBlock_Beschrijving.Text = p.Beschrijving;
			ComboBox_Status.Text = p.Status.ToString();


		}
		private void Button_View_Locatie(object sender, RoutedEventArgs e)
		{

		}

		private void Button_Nieuwe_Locatie(object sender, RoutedEventArgs e)
		{

		}

		private void Button_Opslaan(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!TextBlock_Nieuwe_Titel.Text.IsNullOrEmpty()) p.Titel = TextBlock_Nieuwe_Titel.Text;
				if (Convert.ToDateTime(DatePicker_Nieuwe_Datum) != p.StartDatum) p.StartDatum = Convert.ToDateTime(DatePicker_Nieuwe_Datum);
				if (!TextBlock_Nieuwe_Beschrijving.Text.IsNullOrEmpty()) p.Beschrijving = TextBlock_Nieuwe_Beschrijving.Text;
				if (ComboBox_Nieuwe_Status.Text != p.Status.ToString())
				{

					string status = ComboBox_Nieuwe_Status.Text;
					if (Status.Planning.ToString() == status)
						p.Status = Status.Planning;
					else if (Status.Uitvoering.ToString() == status)
						p.Status = Status.Uitvoering;
					else
						p.Status = Status.Afgerond;
				}
				p.StartDatum = Convert.ToDateTime(DatePicker_Nieuwe_Datum);

			}
			catch (Exception ex)
			{
				MessageBox.Show($"Fout: {ex}");
			}
		}

		private void Button_PartnerKoppelen(object sender, RoutedEventArgs e)
		{
			KoppelPartnersWindow kpw = new KoppelPartnersWindow();
			kpw.ShowDialog();
		}
	}
}
