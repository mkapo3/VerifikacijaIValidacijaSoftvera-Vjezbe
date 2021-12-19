using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivotinjskaFarma
{
    public class Kupovina
    {
        #region Atributi

        string IDKupca;
        DateTime datumKupovine, rokIsporuke;
        Proizvod kupljeniProizvod;
        int kolicina;
        bool popust;
        static int brojac = 1;

        #endregion

        #region Properties

        public string IDKupca1 { get => IDKupca; set => IDKupca = value; }
        public DateTime DatumKupovine { get => datumKupovine; set => datumKupovine = value; }
        public DateTime RokIsporuke { get => rokIsporuke; set => rokIsporuke = value; }
        public Proizvod KupljeniProizvod { get => kupljeniProizvod; set => kupljeniProizvod = value; }
        public int Kolicina { get => kolicina; set => kolicina = value; }
        public bool Popust { get => popust; set => popust = value; }

        #endregion

        #region Konstruktor

        public Kupovina(string id, DateTime datumK, DateTime rok, Proizvod proizvod, int kol, bool popust)
        {
            IDKupca = id;
            DatumKupovine = datumK;
            RokIsporuke = rok;
            KupljeniProizvod = proizvod;
            Kolicina = kol;
            Popust = popust;
        }

        #endregion

        #region Metode

        public bool VerificirajKupovinu()
        {
            if (kolicina > 50)
                return false;
            return true;
        }

        public static int DajSljedeciBroj()
        {
            return brojac++;
        }

        #endregion
    }
}
