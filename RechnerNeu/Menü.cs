using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerNeu
{
    class Menü
    {
        public void Zahl1()
        {
            Console.Write("Eingabe Zahl 1:  ");
        }

        public void Operator()
        {
            Console.Write("Eingabe Operator: ");
        }

        public void Zahl2()
        {
            Console.Write("Eingabe Zahl 2:  ");
        }

        public void FalscheEingabeZahl()
        {
            Console.WriteLine("Du hast einen Falschen wert eingegeben.");
            Console.WriteLine("Gib eine Zahl ein.");
        }

        public void EingabeFalscherOperator()
        {
            Console.WriteLine("Du hast einen Falschen wert eingegeben.");
            Console.WriteLine("Gib einen gültigen Operator ein.");
        }
       
        public void AuswahlMenü()
        {
            Console.WriteLine("[a] - Ergebnis in bruch Schreibweise ");
            Console.WriteLine("[d] - Ergebnis in dezimal Schreibweise ");
            Console.WriteLine("[w] - Weiterrechnen mit Ergebnis ");
            Console.WriteLine("[n] - Neue Rechnung starten");
            Console.WriteLine("[b] - Programm beenden ");
        }

        public void FalscheAuswahl()
        {
            Console.Clear();
            Console.WriteLine("Nutzereingabe war Falsch. Neuer versuch.");
            Console.WriteLine();
        }

        public void Übernahme(decimal ergebnis)
        {
            Console.WriteLine("- Übernahme:" + ergebnis);
        }

        public void ÜbernahmeZahl1(decimal zahl1)
        {
            Console.WriteLine("Zahl1:           " + zahl1);
        }

        public void ÜbernahmeZahl1(string zahl1)
        {
            Console.WriteLine("Zahl1:           " + zahl1);
        }

        public void TeilenDurchNull()
        {
            Console.WriteLine("Man kann nicht durch 0 Teilen.");
            Console.WriteLine("Ergebnis: Undefiniert");
            Console.WriteLine();
        }

        public string NutzerEingabe() => Console.ReadLine();

        public void WeitereAuswahlWeiterRechnen(Bruch ergebnis)
        {
            Console.Clear();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Zahl1 = " + ergebnis.ToString());
            Console.WriteLine("------------------------------------");
        }

        public void WeitereAuswahlAlsBruch(string ergebnis, string umrechnung)
        {
            Console.Clear();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Dezimalzahl: " + umrechnung.ToString() + " Als Bruch: " + ergebnis);
            Console.WriteLine("------------------------------------");
        }

        public void WeitereAuswahlClearConsole()
        {
            Console.Clear();
        }

        public void WeitereAuswahlAlsDezimal(string ergebnis, string umrechnung)
        {
            Console.Clear();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Bruch: " + ergebnis + " in Dezimal: " + umrechnung);
            Console.WriteLine("------------------------------------");
        }

        public void AusgabeErgebnis(string ergebnis)
        {
            Console.WriteLine("Ergebnis = " + ergebnis);
        }
        public void Fehler() => Console.WriteLine("Irgendwas is falsch gelaufen");

        public void Wilkommen()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Herzlich Willkommen,");
            Console.WriteLine("Ihr Taschenrechner steht Ihnen nun zur Verfügung");
            Console.WriteLine("Dezimal Zahlen werden auf 6 stellen gerundet.");
            Console.WriteLine("Eingabe wie folgt: ");
            Console.WriteLine("Eingabe Zahl 1 {Enter} eingabe operator {enter} eingabe zahl2 {enter} ");
            Console.WriteLine("---------------------------------------");
        }

        public void ErgebnisError()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Ergebnis = zu groß um es hier berechnen zu können.");
            Console.WriteLine("Versuchs nochmal.");
            Console.WriteLine("------------------------------------------------");
        }
    }
}
