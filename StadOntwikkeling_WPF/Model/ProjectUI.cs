using StadOntwikkeling_BL.Enums;
using StadOntwikkeling_BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_WPF.Model
{
	public class ProjectUI : INotifyPropertyChanged
	{
		private string _titel;
		private string _beschrijving;
		private DateTime _startdatum;
		private Status _status;
		private LocatieUI _locatie;

		public ProjectUI(int id, string titel, string beschrijving, DateTime startDatum, Status status, LocatieUI locatie, List<ProjectPartner> projecten, List<ProjectOnderdeel> projectOnderdelen)
		{
			Id = id;
			Titel = titel;
			Beschrijving = beschrijving;
			StartDatum = startDatum;
			Status = status;
			Locatie = locatie;
			Projecten = projecten;
			ProjectOnderdelen = projectOnderdelen;
		}

		public ProjectUI(string titel, string beschrijving, DateTime startDatum, Status status, LocatieUI locatie, List<ProjectPartner> projecten, List<ProjectOnderdeel> projectOnderdelen)
		{
			Titel = titel;
			Beschrijving = beschrijving;
			StartDatum = startDatum;
			Status = status;
			Locatie = locatie;
			Projecten = projecten;
			ProjectOnderdelen = projectOnderdelen;
		}

		public int Id { get; set; }
		public string Titel
		{
			get { return _titel; }
			set
			{
				_titel = value;
				OnPropertyChanged(nameof(Titel));
			}
		}
		public string Beschrijving
		{
			get { return _beschrijving; }
			set
			{
				_beschrijving = value;
				OnPropertyChanged(nameof(Beschrijving));
			}
		}
		public DateTime StartDatum
		{
			get { return _startdatum; }
			set
			{
				_startdatum = value;
				OnPropertyChanged(nameof(StartDatum));
			}
		}
		public Status Status
		{
			get { return _status; }
			set
			{
				_status = value;
				OnPropertyChanged(nameof(Status));
			}
		}
		public LocatieUI Locatie
		{
			get { return _locatie; }
			set
			{
				_locatie = value;
				OnPropertyChanged(nameof(Locatie));
			}
		}
		// Moeten worden verandert.
		public List<ProjectPartnerUI> Projecten { get; set; } = new();
		public List<ProjectOnderdeel> ProjectOnderdelen { get; set; } = new();

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
