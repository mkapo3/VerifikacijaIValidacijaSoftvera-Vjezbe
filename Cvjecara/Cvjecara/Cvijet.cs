using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvjecara
{
    public class Cvijet
    {
        #region Atributi

        Vrsta vrsta;
        string latinskoIme, boja;
        DateTime datumBranja;
        bool sezonsko;
        int kolicina;

        #endregion

        #region Properties

        public Vrsta Vrsta { get => vrsta; set => vrsta = value; }
        public string LatinskoIme 
        { 
            get => latinskoIme; 
            set
            {
                if (latinskoIme != null)
                    throw new FormatException("Nemoguće promijeniti latinsko ime cvijeta!");

                if (string.IsNullOrEmpty(value))
                    throw new FormatException("Latinsko ime ne može biti prazan string!");
                latinskoIme = value;
            }
        }
        public string Boja 
        { 
            get => boja;
            set
            {
                List<string> boje = new List<string>()
                { "Žuta", "Crvena", "Bijela", "Roza", "Narandžasta" };

                if (!boje.Contains(value))
                    throw new FormatException("Unijeli ste nepostojeću boju!");

                bool bojeLjiljana = value == "Žuta" || value == "Bijela" || value == "Crvena",
                    bojeNevena = value == "Žuta",
                    bojeMargarete = value == "Žuta" || value == "Bijela",
                    bojeOrhideje = value != "Narandžasta";

                if ((vrsta == Vrsta.Ljiljan && !bojeLjiljana) || (vrsta == Vrsta.Neven && !bojeNevena) ||
                    (vrsta == Vrsta.Margareta && !bojeMargarete) || (vrsta == Vrsta.Orhideja && !bojeOrhideje))
                    throw new FormatException("Unijeli ste pogrešnu boju za zadanu vrstu cvijeća!");

                boja = value;
            }
        }
        public DateTime DatumBranja 
        { 
            get => datumBranja;
            set
            {
                if (value > DateTime.Now)
                    throw new FormatException("Datum branja ne može biti u budućnosti!");
                datumBranja = value;
            }
        }
        public bool Sezonsko { get => sezonsko; set => sezonsko = value; }
        public int Kolicina 
        { 
            get => kolicina;
            set
            {
                if (value < 0)
                    throw new FormatException("Količina ne može biti manja od 1!");
                kolicina = value;
            }
        }

        #endregion

        #region Konstruktor

        public Cvijet(Vrsta vrsta, string ime, string boja, DateTime datumBranja, int kol)
        {
            Vrsta = vrsta;
            LatinskoIme = ime;
            Boja = boja;
            DatumBranja = datumBranja;
            List<string> sezonskeVrste = new List<string>()
            { "Neven", "Margareta", "Ljiljan" };
            Sezonsko = sezonskeVrste.Contains(vrsta.ToString());
            Kolicina = kol;
        }

        #endregion

        #region Metode

        public void ProvjeriKrajSezone()
        {
            if (!sezonsko)
                return;

            int pocetakMjesec = 3,
                krajMjesec = 9;

            int mjesec = DateTime.Now.Month;

            if (mjesec < pocetakMjesec || mjesec > krajMjesec)
                kolicina = 0;
        }

        /// <summary>
        /// Metoda koja se koristi da se odredi koliko je cvijeće svježe.
        /// Ukoliko je cvijeće ubrano prije manje od 3 dana, ima maksimalnu ocjenu (5.0).
        /// Za svaki naredni dan svježina cvijeća se smanjuje za sljedeći koeficijent:
        /// Ruža: 0.2, Neven 0.3, Margareta 0.4, Orhideja 0.5, Ljiljan 0.6.
        /// Svakog idućeg dana koeficijent se uduplava.
        /// Potrebno je vratiti ukupnu ocjenu svježine cvijeća kao rezultat rada funkcije.
        /// Svježina cvijeća ne može biti manja od 0.0.
        /// </summary>
        /// <returns></returns>
        public double OdrediSvježinuCvijeća()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
