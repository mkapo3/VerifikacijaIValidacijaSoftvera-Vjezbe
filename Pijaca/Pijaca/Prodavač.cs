using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pijaca
{
    public class Prodavač
    {
        #region Atributi

        string ime, sigurnosniKod;
        DateTime otvaranjeŠtanda;
        bool aktivnost;
        double ukupniPromet;

        #endregion

        #region Properties

        public string Ime 
        { 
            get => ime;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("Neispravno uneseno ime prodavača!");
                ime = value;
            }
        }
        public DateTime OtvaranjeŠtanda 
        { 
            get => otvaranjeŠtanda;
            set
            {
                if (Math.Abs((DateTime.Now - value).TotalDays) < 30)
                    throw new InvalidOperationException("Štand nije moguće registrovati dok ne prođe barem mjesec dana!");
                else if (Math.Abs((DateTime.Now - value).TotalDays) > 365)
                    throw new InvalidOperationException("Štand nije moguće registrovati - protekao period dozvoljene registracije!");
                otvaranjeŠtanda = value;
            }
        }
        public bool Aktivnost { get => aktivnost; set => aktivnost = value; }
        public double UkupniPromet { get => ukupniPromet; }

        #endregion

        #region Konstruktor

        public Prodavač(string ime, string sigurnosnaŠifra, DateTime otvaranje, double dosadašnjiPromet)
        {
            Ime = ime;
            sigurnosniKod = sigurnosnaŠifra;
            OtvaranjeŠtanda = otvaranje;
            aktivnost = true;
            if (dosadašnjiPromet < 0)
                throw new InvalidOperationException("Promet ne može biti negativan!");
            ukupniPromet = dosadašnjiPromet;
        }

        #endregion

        #region Metode

        public void RegistrujPromet(string šifra, double promet, DateTime početak, DateTime kraj)
        {
            if (šifra != sigurnosniKod)
                throw new ArgumentException("Neispravna šifra!");
            else if (početak < kraj)
                throw new ArgumentException("Neispravan period unošenja prometa!");

            ukupniPromet += promet;
        }

        #endregion
    }
}
