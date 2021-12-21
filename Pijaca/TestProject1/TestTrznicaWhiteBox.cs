using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pijaca;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    [TestClass]
    public class TestTrznicaWhiteBox
    {
       

        static List<Proizvod> proizvodi;
        static List<Kupovina> kupovine;

        [TestInitialize]

        public void Inicijalizacija()
        {

            proizvodi = new List<Proizvod>();
            for (int i = 0; i < 3; i++)
            {
                proizvodi.Add(new Proizvod(Namirnica.Voće, "Kruska", 5, new DateTime(2021, 5, 5), 2, true));
            }
            kupovine = new List<Kupovina>();
            kupovine.Add(new Kupovina(new Proizvod(Namirnica.Voće, "Kruska", 5, new DateTime(2021, 5, 5), 2, true), 3));
            kupovine.Add(new Kupovina(new Proizvod(Namirnica.Meso, "Salama", 6, new DateTime(2021, 5, 2), 2, true), 4));
            kupovine.Add(new Kupovina(new Proizvod(Namirnica.Voće, "Kruska", 8, new DateTime(2021, 5, 15), 2, true), 5));


        }


        //ZADATAK 3
        //WHITEBOX

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNaruciProizvodeRokoviIzuzetak()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac1 = new Prodavač("MuharemJedan", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac1, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac1, proizvodi, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5, 6, 3 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5) };
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodi, kolicine, rokovi);
        }

        [TestMethod]
        public void TestNaruciProizvodeSviFalse()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac1 = new Prodavač("MuharemJedan", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac1, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac1, proizvodi, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 1, 1, 1 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5), new DateTime(2023, 1, 11) };
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodi, kolicine, rokovi);

            for (int i = 0; i < 3; i++)
            {

                Assert.AreEqual(kolicine[i], trznica.Štandovi[0].Proizvodi[i].OčekivanaKoličina);
                Assert.AreEqual(rokovi[i], trznica.Štandovi[0].Proizvodi[i].DatumOčekivaneKoličine);
            }
            
        }

        [TestMethod]
        public void TestNaruciProizvodeSviTrue()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac1 = new Prodavač("MuharemJedan", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac1, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac1, proizvodi, new DateTime(2023, 1, 1));
            List<int> kolicine = new List<int>() { 1, 1, 1 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5), new DateTime(2023, 1, 11) };
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodi, kolicine, rokovi, true);
            for (int i = 0; i < 3; i++)
            { 
                Assert.AreEqual(0, trznica.Štandovi[0].Proizvodi[i].OčekivanaKoličina);
                Assert.IsFalse(rokovi[i] == trznica.Štandovi[0].Proizvodi[i].DatumOčekivaneKoličine);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNaruciProizvodeFaliProizvod()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac1 = new Prodavač("MuharemJedan", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac1, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac1, proizvodi, new DateTime(2023, 1, 1));

            List<Proizvod> proizvodiDrugaciji = new List<Proizvod>();
            for (int i = 0; i < 3; i++)
            {
                proizvodiDrugaciji.Add(new Proizvod(Namirnica.Voće, "Ananas", 5, new DateTime(2021, 5, 5), 2, false));
            }

            List<int> kolicine = new List<int>() { 1, 1, 1 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5), new DateTime(2023, 1, 11) };
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodiDrugaciji, kolicine, rokovi, false);

        }
    }
}
