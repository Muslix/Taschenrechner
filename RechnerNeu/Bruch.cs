using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerNeu
{
    class Bruch
    {
        private double Zähler { get; set; }
        private double Nenner { get; set; }

        public static Bruch Parse(string vorkomma) => new Bruch(double.Parse(vorkomma), 1);
        public static Bruch Parse(double zähler, double nenner) => new Bruch(zähler, nenner);
        public static Bruch Parse(string vorkomma, string nachkomma)
        {
            var zähler = double.Parse(vorkomma);
            double nenner = 1;

            for (var i = 0; i < nachkomma.Length; i++)
            {
                zähler = zähler * 10;
                nenner = nenner * 10;
            }
            zähler = zähler + double.Parse(nachkomma);

            return new Bruch(Math.Round(zähler, 6), Math.Round(nenner, 6));
        }
        public static Bruch Parse(string vorkomma, string nachkomma, string periode)
        {
            var ganzeZahlVorkomma = double.Parse(vorkomma);
            double nenner1 = 1;
            double nenner2 = 9;
            if (nachkomma == "")
            {
                nachkomma = periode;
            }
            var zahl1 = double.Parse(nachkomma);
            var zahl2 = double.Parse(periode);
            for (var i = 0; i < nachkomma.Length; i++)
            {
                nenner1 *= 10;
            }
            for (var j = 1; j < periode.Length; j++)
            {
                nenner2 *= 10;
                nenner2 += 9;
            }
            zahl1 = zahl1 * nenner2;
            nenner2 = nenner2 * nenner1;

            ganzeZahlVorkomma = ganzeZahlVorkomma * nenner2;
            var zähler = zahl1 + zahl2 + ganzeZahlVorkomma;
            var nenner = nenner2;

            return new Bruch(Math.Round(zähler, 6), Math.Round(nenner, 6));
        }

        public double UmrechnungInDezimal(Bruch ergebnisBruch)
        {
            var ergebnisDezimal = ergebnisBruch.Zähler / ergebnisBruch.Nenner;
            return ergebnisDezimal;
        }

        public Bruch(double zähler)
        {
            Zähler = zähler;
            Nenner = 1;
        }

        public Bruch(double zähler, double nenner)
        {
            Nenner = nenner;
            Zähler = zähler;
        }

        private static double GrößterGemeinsamerTeiler(double zähler, double nenner)
        {
            double ggt, rest;

            while (zähler != 0)
            {
                rest = nenner % zähler;
                nenner = zähler;
                zähler = rest;
                if (double.IsNaN(nenner) || double.IsInfinity(nenner))
                {
                    zähler = 0;
                }

            }
            ggt = nenner;
            return ggt;
        }

        private static Tuple<double, double> Kürzen(double zähler, double nenner)
        {
            var neuerZähler = zähler / GrößterGemeinsamerTeiler(zähler, nenner);
            var neuerNenner = nenner / GrößterGemeinsamerTeiler(zähler, nenner);

            return new Tuple<double, double>(neuerZähler, neuerNenner);
        }

        public static Bruch operator +(Bruch a, Bruch b)
        {
            var r1 = a.Zähler * b.Nenner + b.Zähler * a.Nenner;
            var r2 = a.Nenner * b.Nenner;
            var ergebnis = Kürzen(r1, r2);

            if (double.IsInfinity(ergebnis.Item1) || double.IsInfinity(ergebnis.Item2) || double.IsNaN(ergebnis.Item1) || double.IsNaN(ergebnis.Item2))
            {
                return null;
            }
            return new Bruch(ergebnis.Item1, ergebnis.Item2);
        }

        public static Bruch operator -(Bruch a, Bruch b)
        {
            var r1 = a.Zähler * b.Nenner - b.Zähler * a.Nenner;
            var r2 = a.Nenner * b.Nenner;
            var ergebnis = Kürzen(r1, r2);

            if (double.IsInfinity(ergebnis.Item1) || double.IsInfinity(ergebnis.Item2) || double.IsNaN(ergebnis.Item1) || double.IsNaN(ergebnis.Item2))
            {
                return null;
            }
            return new Bruch(ergebnis.Item1, ergebnis.Item2);
        }

        public static Bruch operator *(Bruch a, Bruch b)
        {
            var r1 = a.Zähler * b.Zähler;
            var r2 = a.Nenner * b.Nenner;
            var ergebnis = Kürzen(r1, r2);

            if (double.IsInfinity(ergebnis.Item1) || double.IsInfinity(ergebnis.Item2) || double.IsNaN(ergebnis.Item1) || double.IsNaN(ergebnis.Item2))
            {
                return null;
            }
            return new Bruch(ergebnis.Item1, ergebnis.Item2);
        }

        public static Bruch operator /(Bruch a, Bruch b)
        {
            var r1 = a.Zähler * b.Nenner;
            var r2 = a.Nenner * b.Zähler;
            var ergebnis = Kürzen(r1, r2);
            if (double.IsInfinity(ergebnis.Item1) || double.IsInfinity(ergebnis.Item2) || double.IsNaN(ergebnis.Item1) || double.IsNaN(ergebnis.Item2))
            {
                return null;
            }
            return new Bruch(ergebnis.Item1, ergebnis.Item2);
        }

        public override string ToString()
        {
            if (Nenner == 0)
            {
                return "Devide by Zero";
            }
            string zählerFormatiert = string.Format("{0:0.##}", Zähler);
            string nennerFormatiert = string.Format("{0:0.##}", Nenner);
            return zählerFormatiert + "/" + nennerFormatiert;
        }
    }
}