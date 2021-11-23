
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvjecara
{
    public class Cvjećara
    {
        #region Atributi

        List<Cvijet> cvijeće;
        List<Buket> buketi;
        List<Mušterija> mušterije;
        List<Poklon> naručeniPokloni;

        #endregion

        #region Properties

        public List<Cvijet> Cvijeće { get => cvijeće; }
        public List<Poklon> NaručeniPokloni { get => naručeniPokloni; set => naručeniPokloni = value; }
        public List<Mušterija> Mušterije { get => mušterije; set => mušterije = value; }

        #endregion

        #region Konstruktor

        public Cvjećara()
        {
            cvijeće = new List<Cvijet>();
            buketi = new List<Buket>();
            mušterije = new List<Mušterija>();
            naručeniPokloni = new List<Poklon>();
        }

        #endregion

        #region Metode

        public void RadSaCvijećem(Cvijet c, int opcija)
        {
            if (opcija == 0)
            {
                if (c == null)
                    throw new NullReferenceException("Nemoguće dodati cvijet koji ne postoji!");
                else if (cvijeće.Contains(c))
                    throw new InvalidOperationException("Nemoguće dodati cvijet koji već postoji!");
                else
                    cvijeće.Add(c);
            }
            else if (opcija == 1)
            {
                if (c == null)
                    throw new NullReferenceException("Nemoguće izmijeniti cvijet koji ne postoji!");
                else if (cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme) == null)
                    throw new InvalidOperationException("Nemoguće izmijeniti cvijet koji ne postoji!");
                else
                {
                    cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme));
                    cvijeće.Add(c);
                }
            }
            else if (opcija == 2)
            {
                if (c == null)
                    throw new NullReferenceException("Nemoguće obrisati cvijet koji ne postoji!");
                else if (cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme) == null)
                    throw new InvalidOperationException("Nemoguće obrisati cvijet koji ne postoji!");
                else
                {
                    cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme));
                }
            }
            else
                throw new InvalidOperationException("Unijeli ste nepoznatu opciju!");
        }

        public void DodajBuket(List<Cvijet> cvijeće, List<string> dodaci, Poklon poklon, double cijena)
        {
            Buket b = new Buket(cijena);
            b.DodajPoklon(poklon);
            foreach (Cvijet c in cvijeće)
                b.DodajCvijet(c);
            foreach (string dodatak in dodaci)
                b.DodajDodatak(dodatak);
            buketi.Add(b);
        }

        public bool ObrišiBuket(Buket b)
        {
            return buketi.Remove(b);
        }

        public List<Buket> DajSveBukete()
        {
            return buketi;
        }

        public void IzvršiNabavku(string godišnjeDoba, string veličinaNarudžbe)
        {
            throw new NotImplementedException();
        }

        public void PregledajCvijeće()
        {
            foreach (Cvijet cvijet in cvijeće)
            {
                cvijet.ProvjeriKrajSezone();
                if (cvijet.OdrediSvježinuCvijeća() < 2)
                    cvijet.Kolicina = 0;
            }

            cvijeće.RemoveAll(cvijet => cvijet.Kolicina == 0);
        }

        public void NaručiCvijeće(Mušterija m, Buket b, Poklon p, Poklon nagrada = null)
        {
            if (!buketi.Contains(b))
                throw new InvalidOperationException("Traženi buket nije na stanju!");

            m.RegistrujKupovinu(b, p);
            naručeniPokloni.Add(p);

            if (nagrada != null)
                m.NagradnaKupovina(p);
        }

        public void ProvjeriLatinskaImenaCvijeća(ILeksikon leksikon)
        {
            List<Cvijet> zaObrisati = new List<Cvijet>();
            foreach (Cvijet c in cvijeće)
            {
                if (!leksikon.ValidnoLatinskoIme(c.LatinskoIme))
                    zaObrisati.Add(c);
            }
            cvijeće.RemoveAll(cvijet => zaObrisati.Contains(cvijet));
        }

        public List<Poklon> DajSveNaručenePoklone(Mušterija m, double popust)
        {
            List<Poklon> pokloni = m.KupljeniPokloni.FindAll(poklon => poklon.PostotakPopusta == popust);
            if (pokloni == null || pokloni.Count == 0)
                throw new FormatException("Došlo je do greške! Pokušajte ponovo sa drugim parametrima zahtjeva.");

            return pokloni;
        }

        /// <summary>
        /// Metoda koja vraća mušteriju koja je izvršila najveći broj kupovina.
        /// Ukoliko cvjećara nema nijednu mušteriju, potrebno je baciti izuzetak.
        /// U suprotnom, potrebno je pronaći mušteriju koja je ukupno kupila najviše cvijeća
        /// u svim buketima koje je naručila.
        /// Ukoliko postoji više takvih mušterija, potrebno je vratiti onu mušteriju
        /// koja je potrošila veći ukupni iznos novca na sve kupljene bukete.
        /// Ukoliko i u tom slučaju postoji više mušterija, potrebno je baciti izuzetak
        /// jer se najbolja mušterija u tom slučaju ne može tačno odrediti.
        /// </summary>
        /// <returns></returns>
        public Mušterija DajNajboljuMušteriju()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
