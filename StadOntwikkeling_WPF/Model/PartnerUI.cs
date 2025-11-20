using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_WPF.Model
{
	public class PartnerUI : INotifyPropertyChanged
	{
		private string _naam;
		private LocatieUI _locatie;
		private string _email;
		public int Id { get; set; }

		public string Naam
		{
			get { return _naam; }
			set
			{
				_naam = value;
				OnPropertyChanged(nameof(Naam));
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
		public string Email
		{
			get { return _email; }
			set
			{
				_email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
