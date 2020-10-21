using System;

namespace DataReader
{
    class AAFData
    {
        public DateTime Timestamp { get; private set; }
        public string Pcnavn { get; private set; }
        public string Primaerbruger { get; private set; }
        public string Navn { get; private set; }
        public string Afdeling { get; private set; }
        public string Operativsystem { get; private set; }
        public int CPU { get; private set; }
        public int RAM { get; private set; }
        public int HDD { get; private set; }
        public string Servicepack { get; private set; }
        public string Serienummer { get; private set; }
        public DateTime Fakturadato { get; private set; }
        public string Gateway { get; private set; }
        public string Model { get; private set; }
        public string Pctype { get; private set; }

        public AAFData(DateTime timestamp, string pcnavn, string primaerbruger, string navn, string afdeling, string operativsystem, int cPU, int rAM, int hDD, string servicepack, string serienummer, DateTime fakturadato, string gateway, string model, string pctype)
        {
            Timestamp = timestamp;
            Pcnavn = pcnavn;
            Primaerbruger = primaerbruger;
            Navn = navn;
            Afdeling = afdeling;
            Operativsystem = operativsystem;
            CPU = cPU;
            RAM = rAM;
            HDD = hDD;
            Servicepack = servicepack;
            Serienummer = serienummer;
            Fakturadato = fakturadato;
            Gateway = gateway;
            Model = model;
            Pctype = pctype;
        }
    }
}
