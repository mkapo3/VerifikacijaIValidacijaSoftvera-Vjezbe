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
        /*
        
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
        
        // ARMIN BEGIC
        // Obuhvat uslova 
        // proizvodi.Count != količine.Count | proizvodi.Count != rokovi.Count | !svi | pr == null |
        //                T                  |                  X              |   X  |      X     | 1
        //                N                  |                  T              |   X  |      X     | 2
        //                N                  |                  N              |   N  |      X     | 3
        //                N                  |                  N              |   T  |      T     | 4
        //                N                  |                  N              |   T  |      N     | 5


        // ARMIN BEGIC 1
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNaruciProizvodeOU1()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac, proizvodi, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5, 6, 3, 1};
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5), new DateTime(2023, 1, 10) };
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodi, kolicine, rokovi, false );
        }

        // ARMIN BEGIC 2
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNaruciProizvodeOU2()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac, proizvodi, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5, 6, 3 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5)};
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodi, kolicine, rokovi, false );
        }

        // ARMIN BEGIC 3
        [TestMethod]
        public void TestNaruciProizvodeOU3()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac, proizvodi, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5, 6, 3 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5), new DateTime(2023, 1, 10) };
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodi, kolicine, rokovi, true);

            Assert.AreEqual(0, trznica.Štandovi[0].Proizvodi[0].OčekivanaKoličina);
            Assert.AreEqual(0, trznica.Štandovi[0].Proizvodi[1].OčekivanaKoličina);
            Assert.AreEqual(0, trznica.Štandovi[0].Proizvodi[2].OčekivanaKoličina);
        }

        // ARMIN BEGIC 4
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNaruciProizvodeOU4()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac, new List<Proizvod>(), new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5, 6, 3 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5), new DateTime(2023, 1, 10) };
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodi, kolicine, rokovi, false);
        }

        // ARMIN BEGIC 5
        [TestMethod]
        public void TestNaruciProizvodeOU5()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac, proizvodi, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5, 6, 3 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5), new DateTime(2023, 1, 10) };
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodi, kolicine, rokovi, false);

            Assert.AreEqual(kolicine[0], trznica.Štandovi[0].Proizvodi[0].OčekivanaKoličina);
            Assert.AreEqual(rokovi[0], trznica.Štandovi[0].Proizvodi[0].DatumOčekivaneKoličine);

            Assert.AreEqual(kolicine[1], trznica.Štandovi[0].Proizvodi[1].OčekivanaKoličina);
            Assert.AreEqual(rokovi[1], trznica.Štandovi[0].Proizvodi[1].DatumOčekivaneKoličine);

            Assert.AreEqual(kolicine[2], trznica.Štandovi[0].Proizvodi[2].OčekivanaKoličina);
            Assert.AreEqual(rokovi[2], trznica.Štandovi[0].Proizvodi[2].DatumOčekivaneKoličine);
        }

        */

        //ismail ičanovic 

 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNaruciProizvodeOU11()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac, proizvodi, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5, 6, 3, 1 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5), new DateTime(2023, 1, 10) };
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodi, kolicine, rokovi, false);
        }

 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNaruciProizvodeOU22()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac, proizvodi, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5, 6, 3 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 5) };
            trznica.NaručiProizvode(trznica.Štandovi[0], proizvodi, kolicine, rokovi, false);
        }

        [TestMethod]
        public void TestNaruciProizvodeOU33()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac, new List<Proizvod>() {}, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { };
            List<DateTime> rokovi = new List<DateTime>() { };
            trznica.NaručiProizvode(trznica.Štandovi[0], new List<Proizvod>() { }, kolicine, rokovi, true);

            Assert.AreEqual(0, trznica.Štandovi[0].Proizvodi.Count);

        }

        [TestMethod]
        public void TestNaruciProizvodeOU44()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            Proizvod p = new Proizvod(Namirnica.Voće, "Kruska", 5, new DateTime(2021, 5, 5), 2, true);
            trznica.OtvoriŠtand(prodavac, new List<Proizvod>() { p }, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10) };
            trznica.NaručiProizvode(trznica.Štandovi[0], new List<Proizvod>() { p }, kolicine, rokovi, true);

            Assert.AreEqual(0, trznica.Štandovi[0].Proizvodi[0].OčekivanaKoličina);

        }


        [TestMethod]
        public void TestNaruciProizvodeOU55()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            Proizvod p = new Proizvod(Namirnica.Voće, "Kruska", 5, new DateTime(2021, 5, 5), 2, true);
            trznica.OtvoriŠtand(prodavac, new List<Proizvod>() { p }, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10) };
            trznica.NaručiProizvode(trznica.Štandovi[0], new List<Proizvod>() { p }, kolicine, rokovi, false);

            Assert.AreEqual(5, trznica.Štandovi[0].Proizvodi[0].OčekivanaKoličina);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNaruciProizvodeOU66()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            trznica.OtvoriŠtand(prodavac, new List<Proizvod>() { new Proizvod(Namirnica.Voće, "Kruska", 5, new DateTime(2021, 5, 5), 2, true) }, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10) };
            trznica.NaručiProizvode(trznica.Štandovi[0], new List<Proizvod>() { new Proizvod(Namirnica.Voće, "Kruska", 5, new DateTime(2021, 5, 5), 2, true) }, kolicine, rokovi, false);

            Assert.AreEqual(0, trznica.Štandovi[0].Proizvodi[0].OčekivanaKoličina);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNaruciProizvodeOU77()
        {
            Tržnica trznica = new Tržnica();
            Prodavač prodavac = new Prodavač("Armin", "Sifra", DateTime.Now.AddDays(-45), 40);
            trznica.RadSaProdavačimaRefaktoring(prodavac, "Dodavanje", 200);
            Proizvod p = new Proizvod(Namirnica.Voće, "Kruska", 5, new DateTime(2021, 5, 5), 2, true);
            trznica.OtvoriŠtand(prodavac, new List<Proizvod>() { p,new Proizvod(Namirnica.Voće, "Kruska", 5, new DateTime(2021, 5, 5), 2, true) }, new DateTime(2023, 1, 1));

            List<int> kolicine = new List<int>() { 5 };
            List<DateTime> rokovi = new List<DateTime>() { new DateTime(2023, 1, 10), new DateTime(2023, 1, 10) };
            trznica.NaručiProizvode(trznica.Štandovi[0], new List<Proizvod>() { p, new Proizvod(Namirnica.Voće, "Kruska", 5, new DateTime(2021, 5, 5), 2, true) }, kolicine, rokovi, false);

        }

        
    }
}
