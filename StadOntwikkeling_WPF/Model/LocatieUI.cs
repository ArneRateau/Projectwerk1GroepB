using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadOntwikkeling_WPF.Model
{
	public class LocatieUI : INotifyPropertyChanged
	{
		private string _straat;
		private string _postcode;
		private string _gemeente;
		private string _wijk;
		private string _huisnummer;

		public LocatieUI(int id, string straat, string postcode, string gemeente, string wijk, string huisnummer)
		{
			Id = id;
			Straat = straat;
			Postcode = postcode;
			Gemeente = gemeente;
			Wijk = wijk;
			Huisnummer = huisnummer;
		}

		public LocatieUI(string straat, string postcode, string gemeente, string wijk, string huisnummer)
		{
			Straat = straat;
			Postcode = postcode;
			Gemeente = gemeente;
			Wijk = wijk;
			Huisnummer = huisnummer;
		}

		public int Id { get; set; }

		public string Straat
		{
			get { return _straat; }
			set
			{
				_straat = value;
				OnPropertyChanged(nameof(Straat));
			}
		}
		public string Postcode
		{
			get { return _postcode; }
			set
			{
				_postcode = value;
				OnPropertyChanged(nameof(Postcode));

			}
		}
		public string Gemeente
		{
			get { return _gemeente; }
			set
			{
				_gemeente = value;
				OnPropertyChanged(nameof(Gemeente));

			}
		}
		public string Wijk
		{
			get { return _wijk; }
			set
			{
				_wijk = value;
				OnPropertyChanged(nameof(Wijk));

			}
		}
		public string Huisnummer
		{
			get { return _huisnummer; }
			set
			{
				_huisnummer = value;
				OnPropertyChanged(nameof(Huisnummer));

			}
		}

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
