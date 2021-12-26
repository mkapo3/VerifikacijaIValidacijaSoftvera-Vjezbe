using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pijaca
{
    public class Tržnica
    {
        #region Atributi

        List<Prodavač> prodavači;
        List<Štand> štandovi;
        double ukupniPrometPijace;

        #endregion

        #region Properties

        public List<Prodavač> Prodavači { get => prodavači;}
        public List<Štand> Štandovi { get => štandovi; }
        public double UkupniPrometPijace { get => ukupniPrometPijace; }

        #endregion

        #region Konstruktor

        public Tržnica()
        {
            prodavači = new List<Prodavač>();
            štandovi = new List<Štand>();
            ukupniPrometPijace = 0.0;
        }

        #endregion

        #region Metode

        public void RadSaProdavačima(Prodavač p, string opcija, double najmanjiPromet)
        {
            if (p == null)
                throw new ArgumentNullException("Morate unijeti informacije o prodavaču!");

            Prodavač postojeći = null;
            foreach (var prodavač in prodavači)
            {
                if (prodavač.Ime == p.Ime)
                {
                    if (p.UkupniPromet < najmanjiPromet)
                        continue;
                    else if (prodavač.UkupniPromet < najmanjiPromet)
                        continue;
                    else if (prodavač.UkupniPromet == p.UkupniPromet)
                        postojeći = prodavač;
                }
            }
            if (opcija == "Dodavanje")
            {
                if (postojeći == null || prodavači.FindAll(prod => prod.Ime == p.Ime).Count == 0)
                    throw new InvalidOperationException("Nemoguće dodati prodavača kad već postoji registrovan!");
                else
                    prodavači.Add(p);
            }
            else if (opcija == "Izmjena" || opcija == "Brisanje")
            {
                if (postojeći == null || prodavači.FindAll(prod => prod.Ime == p.Ime).Count == 0)
                    throw new FormatException("Nemoguće izmijeniti tj. obrisati prodavača koji nije registrovan!");
                else
                {
                    prodavači.Remove(postojeći);
                    if (opcija == "Izmjena")
                        prodavači.Add(p);
                }
            }
            else
                throw new InvalidOperationException("Unijeli ste nepoznatu opciju!");
        }
        //ismail icanovic refaktoring

        public void RadSaProdavačimaRefactoring(Prodavač p, string opcija, double najmanjiPromet)
        {
            if (p == null)
                throw new ArgumentNullException("Morate unijeti informacije o prodavaču!");

            if (opcija == "Dodavanje")
            {
                if (p.UkupniPromet < najmanjiPromet || prodavači.FindAll(prod => prod.Ime == p.Ime && prod.UkupniPromet == p.UkupniPromet).Count > 0)
                    throw new InvalidOperationException("Nemoguće dodati prodavača kad već postoji registrovan!");
                else
                    prodavači.Add(p);
            }
            else if (opcija == "Izmjena" || opcija == "Brisanje")
            {
                if (prodavači.FindAll(prod => prod.Ime == p.Ime).Count == 0)
                    throw new FormatException("Nemoguće izmijeniti tj. obrisati prodavača koji nije registrovan!");
                else
                {
                    prodavači.Remove(prodavači.Find(prod => prod.Ime == p.Ime && prod.UkupniPromet == p.UkupniPromet));
                    if (opcija == "Izmjena")
                        prodavači.Add(p);
                }
            }
            else
                throw new InvalidOperationException("Unijeli ste nepoznatu opciju!");
        }

        public void OtvoriŠtand(Prodavač p, List<Proizvod> pr, DateTime rok)
        {
            if (!prodavači.Contains(p))
                throw new ArgumentException("Prodavač nije registrovan!");
            if (štandovi.Find(š => š.Prodavač == p) != null)
                throw new FormatException("Prodavač može imati samo jedan štand!");
            Štand štand = new Štand(p, rok, pr);
            štandovi.Add(štand);
        }

        
        public void IzvršavanjeKupovina(Štand š, List<Kupovina> kupovine, string sigurnosniKod)
        {
            Štand štand = štandovi.Find(št => št.Prodavač == š.Prodavač);
            if (štand == null)
                throw new ArgumentException("Unijeli ste štand koji nije registrovan!");

            DateTime najranijaKupovina = kupovine[0].DatumKupovine, najkasnijaKupovina = kupovine[0].DatumKupovine;
            double ukupanPromet = 0;

            foreach (var kupovina in kupovine)
            {
                if (kupovina.DatumKupovine < najranijaKupovina)
                    najranijaKupovina = kupovina.DatumKupovine;
                if (kupovina.DatumKupovine > najkasnijaKupovina)
                    najkasnijaKupovina = kupovina.DatumKupovine;
                ukupanPromet += kupovina.UkupnaCijena;

                štand.RegistrujKupovinu(kupovina);
            }

            Prodavač prodavač = štand.Prodavač;
            prodavač.RegistrujPromet(sigurnosniKod, ukupanPromet, najranijaKupovina, najkasnijaKupovina);
        }

        // REFAKTORING
        // ARMIN BEGIC
        public void IzvršavanjeKupovina1(Štand š, List<Kupovina> kupovine, string sigurnosniKod)
        {
            Štand štand = štandovi.Find(št => št.Prodavač == š.Prodavač);
            if (štand == null)
                throw new ArgumentException("Unijeli ste štand koji nije registrovan!");

            DateTime najranijaKupovina = kupovine[0].DatumKupovine, najkasnijaKupovina = kupovine[0].DatumKupovine;
            double ukupanPromet = 0;

            foreach (var kupovina in kupovine)
            {
                if (kupovina.DatumKupovine < najranijaKupovina)
                    najranijaKupovina = kupovina.DatumKupovine;
                else if (kupovina.DatumKupovine > najkasnijaKupovina)
                    najkasnijaKupovina = kupovina.DatumKupovine;
                ukupanPromet += kupovina.UkupnaCijena;

                štand.RegistrujKupovinu(kupovina);
            }

            štand.Prodavač.RegistrujPromet(sigurnosniKod, ukupanPromet, najranijaKupovina, najkasnijaKupovina);
        }

        public void NaručiProizvode(Štand štand, List<Proizvod> proizvodi, List<int> količine, List<DateTime> rokovi, bool svi = false)
        {
            if (proizvodi.Count != količine.Count || proizvodi.Count != rokovi.Count)
                throw new ArgumentException("Pogrešan unos parametara!");
            
            for (int i = 0; i < proizvodi.Count; i++)
            {
                if (!svi)
                {
                    Proizvod pr = štand.Proizvodi.Find(p => p.ŠifraProizvoda == proizvodi[i].ŠifraProizvoda);
                    if (pr == null)
                        throw new ArgumentException("Nemoguće naručiti proizvod - nije registrovan na štandu!");

                    pr.NaručiKoličinu(količine[i], rokovi[i]);
                }
                else
                    continue;
            }
        }
        //REFAKTORING
        //MUHAREM KAPO
        //uklonjen nepotrebni else continue
        public void NaručiProizvode1(Štand štand, List<Proizvod> proizvodi, List<int> količine, List<DateTime> rokovi)
        {
            if (proizvodi.Count != količine.Count || proizvodi.Count != rokovi.Count)
                throw new ArgumentException("Pogrešan unos parametara!");

            for (int i = 0; i < proizvodi.Count; i++)
            {
                 Proizvod pr = štand.Proizvodi.Find(p => p.ŠifraProizvoda == proizvodi[i].ŠifraProizvoda);
                 if (pr == null)
                    throw new ArgumentException("Nemoguće naručiti proizvod - nije registrovan na štandu!");

                 pr.NaručiKoličinu(količine[i], rokovi[i]);
            }
        }

        
        //promijenjen uslov u postojeci!=null i ...Count!=0 u Dodavanje da bi metoda mogla raditi
        public void RadSaProdavačimaRefaktoring(Prodavač p, string opcija, double najmanjiPromet)
        {
            if (p == null)
                throw new ArgumentNullException("Morate unijeti informacije o prodavaču!");

            Prodavač postojeći = null;
            foreach (var prodavač in prodavači)
            {
                if (prodavač.Ime == p.Ime)
                {
                    if (p.UkupniPromet < najmanjiPromet)
                        continue;
                    else if (prodavač.UkupniPromet < najmanjiPromet)
                        continue;
                    else if (prodavač.UkupniPromet == p.UkupniPromet)
                        postojeći = prodavač;
                }
            }
            if (opcija == "Dodavanje")
            {
                if (postojeći != null || prodavači.FindAll(prod => prod.Ime == p.Ime).Count != 0)
                    throw new InvalidOperationException("Nemoguće dodati prodavača kad već postoji registrovan!");
                else
                    prodavači.Add(p);
            }
            else if (opcija == "Izmjena" || opcija == "Brisanje")
            {
                if (postojeći == null || prodavači.FindAll(prod => prod.Ime == p.Ime).Count == 0)
                    throw new FormatException("Nemoguće izmijeniti tj. obrisati prodavača koji nije registrovan!");
                else
                {
                    prodavači.Remove(postojeći);
                    if (opcija == "Izmjena")
                        prodavači.Add(p);
                }
            }
            else
                throw new InvalidOperationException("Unijeli ste nepoznatu opciju!");
        }

        //MUHAREM KAPO
        //Izbaceni nepotrebna grananja u foreach
        public void RadSaProdavačimaCodeTuning1(Prodavač p, string opcija, double najmanjiPromet)
        {
            if (p == null)
                throw new ArgumentNullException("Morate unijeti informacije o prodavaču!");

            Prodavač postojeći = null;
            foreach (var prodavač in prodavači)
            {
                if (prodavač.Ime == p.Ime)
                {
                    if (prodavač.UkupniPromet == p.UkupniPromet)
                        postojeći = prodavač;
                }
            }
            if (opcija == "Dodavanje")
            {
                if (postojeći != null || prodavači.FindAll(prod => prod.Ime == p.Ime).Count != 0)
                    throw new InvalidOperationException("Nemoguće dodati prodavača kad već postoji registrovan!");
                else
                    prodavači.Add(p);
            }
            else if (opcija == "Izmjena" || opcija == "Brisanje")
            {
                if (postojeći == null || prodavači.FindAll(prod => prod.Ime == p.Ime).Count == 0)
                    throw new FormatException("Nemoguće izmijeniti tj. obrisati prodavača koji nije registrovan!");
                else
                {
                    prodavači.Remove(postojeći);
                    if (opcija == "Izmjena")
                        prodavači.Add(p);
                }
            }
            else
                throw new InvalidOperationException("Unijeli ste nepoznatu opciju!");
        }

        // ARMIN BEGIC
        // Izbacivanje kompleksnog uslova iz if - tuning logičkih iskaza 
        // Dodana varijabla brojProdavačaImena koja unutar postojeće foreach petlje dodaje i broj prodavača istog imena
        // Ssmanjena kompleksnost if uslova, i izbjegnut dodatni prolazak kroz listu prodavača

        public void RadSaProdavačimaCodeTuning2(Prodavač p, string opcija, double najmanjiPromet)
        {
            if (p == null)
                throw new ArgumentNullException("Morate unijeti informacije o prodavaču!");

            Prodavač postojeći = null;
            int brojProdavačaImena = 0; 
            foreach (var prodavač in prodavači)
            {
                if (prodavač.Ime == p.Ime)
                {
                    brojProdavačaImena++;
                    if (prodavač.UkupniPromet == p.UkupniPromet)
                        postojeći = prodavač;
                }
            }


            if (opcija == "Dodavanje")
            {
                if (postojeći != null || brojProdavačaImena != 0)
                    throw new InvalidOperationException("Nemoguće dodati prodavača kad već postoji registrovan!");
                else
                    prodavači.Add(p);
            }
            else if (opcija == "Izmjena" || opcija == "Brisanje")
            {
                if (postojeći == null || brojProdavačaImena == 0)
                    throw new FormatException("Nemoguće izmijeniti tj. obrisati prodavača koji nije registrovan!");
                else
                {
                    prodavači.Remove(postojeći);
                    if (opcija == "Izmjena")
                        prodavači.Add(p);
                }
            }
            else
                throw new InvalidOperationException("Unijeli ste nepoznatu opciju!");
        }



        //ismail icanovic CodeTunung

        public void RadSaProdavačimaCodeTuning3(Prodavač p, string opcija, double najmanjiPromet)
        {
            if (p == null)
                throw new ArgumentNullException("Morate unijeti informacije o prodavaču!");

            Prodavač postojeci = null;
            postojeci = prodavači.Find(prod => prod.Ime == p.Ime && prod.UkupniPromet == p.UkupniPromet);

            if (opcija == "Dodavanje")
            {
                if (postojeci != null)
                    throw new InvalidOperationException("Nemoguće dodati prodavača kad već postoji registrovan!");
                else
                    prodavači.Add(p);
            }
            else if (opcija == "Izmjena" || opcija == "Brisanje")
            {
                if (postojeci == null)
                    throw new FormatException("Nemoguće izmijeniti tj. obrisati prodavača koji nije registrovan!");
                else
                {
                    prodavači.Remove(postojeci);
                    if (opcija == "Izmjena")
                        prodavači.Add(p);
                }
            }
            else
                throw new InvalidOperationException("Unijeli ste nepoznatu opciju!");
        }


        #endregion
    }
}
