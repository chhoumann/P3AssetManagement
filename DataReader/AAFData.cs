using System;

namespace DataReader
{
    public struct AAFData
    {
        public DateTime Timestamp { get; }
        public string PcNavn { get; }
        public string PrimaerBruger { get; }
        public string Navn { get; }
        public string Afdeling { get; }
        public string OperativSystem { get; }
        public int CPU { get; }
        public int RAM { get; }
        public int HDD { get; }
        public string ServicePack { get; }
        public string SerieNummer { get; }
        public DateTime FakturaDato { get; }
        public string Gateway { get; }
        public string Model { get; }
        public string PcType { get; }

        public AAFData(DateTime timestamp, string pcnavn, string primaerbruger, string navn, string afdeling, string operativsystem, int cpu, int ram, int hdd, string servicepack, string serienummer, DateTime fakturadato, string gateway, string model, string pctype)
        {
            Timestamp = timestamp;
            PcNavn = pcnavn;
            PrimaerBruger = primaerbruger;
            Navn = navn;
            Afdeling = afdeling;
            OperativSystem = operativsystem;
            CPU = cpu;
            RAM = ram;
            HDD = hdd;
            ServicePack = servicepack;
            SerieNummer = serienummer;
            FakturaDato = fakturadato;
            Gateway = gateway;
            Model = model;
            PcType = pctype;
        }
    }
}
