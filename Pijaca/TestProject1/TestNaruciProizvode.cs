using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pijaca;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    [TestClass]
    public class TestNaruciProizvode
    {
        [TestMethod]
        public void TestRadSaProdavacimaNoCT()
        {
            Tržnica trznica = new Tržnica();
            for (int i = 0; i < 50000; i++)
            {
                Prodavač prodavac1 = new Prodavač("MuharemJedan" + i, "Sifra", DateTime.Now.AddDays(-45), 40);
                trznica.RadSaProdavačimaRefaktoring(prodavac1, "Dodavanje", 100+i);
            }
        }

        [TestMethod]
        public void TestRadSaProdavacimaCodeTuning1()
        {
            Tržnica trznica = new Tržnica();
            for (int i = 0; i < 50000; i++)
            {
                Prodavač prodavac1 = new Prodavač("MuharemJedan" + i, "Sifra", DateTime.Now.AddDays(-45), 40);
                trznica.RadSaProdavačimaCodeTuning1(prodavac1, "Dodavanje", 100 + i);
            }
        }
    }
}
