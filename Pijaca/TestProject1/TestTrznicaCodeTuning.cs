using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pijaca;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    [TestClass]
    public class TestTrznicaCodeTuning
    {

        //ZADATAK 2
        //CODETUNING

        [TestMethod]
        public void TestRadSaProdavacimaNoCT()
        {
            Tržnica trznica = new Tržnica();
            for (int i = 0; i < 50000; i++)
            {
                Prodavač prodavac1 = new Prodavač("MuharemJedan" + i, "Sifra", DateTime.Now.AddDays(-45), 40);
                trznica.RadSaProdavačimaRefaktoring(prodavac1, "Dodavanje", 100 + i);
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

        [TestMethod]
        public void TestRadSaProdavacimaCodeTuning2()
        {
            Tržnica trznica = new Tržnica();
            for (int i = 0; i < 50000; i++)
            {
                Prodavač prodavac1 = new Prodavač("MuharemJedan" + i, "Sifra", DateTime.Now.AddDays(-45), 40);
                trznica.RadSaProdavačimaCodeTuning2(prodavac1, "Dodavanje", 100 + i);
            }
        }


        [TestMethod]
        public void TestRadSaProdavacimaCodeTuning3()
        {
            Tržnica trznica = new Tržnica();
            for (int i = 0; i < 50000; i++)
            {
                Prodavač prodavac1 = new Prodavač("Ismail" + i, "Sifra", DateTime.Now.AddDays(-45), 40);
                
                trznica.RadSaProdavačimaCodeTuning3(prodavac1, "Dodavanje", 100 + i);
            }
        }

    }
}
