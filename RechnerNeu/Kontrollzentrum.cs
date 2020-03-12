using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerNeu
{
    class Kontrollzentrum
    {
        Menü menü = new Menü();       

        private Dictionary<string, Operand> EingabeMap { get; } = new Dictionary<string, Operand>()
        {
            {"+", Operand.Add },
            {"-", Operand.Sub },
            {"*", Operand.Multi },
            {"/", Operand.Teilen }
        };

        private Dictionary<string, Auswahl> AuswahlMap { get; } = new Dictionary<string, Auswahl>()
        {
            {"w", Auswahl.WeiterRechnen },
            {"a", Auswahl.AlsBruch },
            {"n", Auswahl.NeueRechnung },
            {"d", Auswahl.AlsDezimal },
            {"b", Auswahl.Beenden }
        };

        public void NutzerEingabe()
        {
            var parser = new Parser();
            menü.Wilkommen();
            Operand operand;
            Bruch bruchLinks;
            Bruch bruchRechts;
            string eingabe;
            string eingabeOperator;

            do
            {
                menü.Zahl1();
                eingabe = menü.NutzerEingabe();
            } while (!parser.TryParseBruch(eingabe, out bruchLinks));

            do
            {
                menü.Operator();
                eingabeOperator = menü.NutzerEingabe();
            } while (!parser.IstOperator(eingabeOperator));

            do
            {
                menü.Zahl2();
                eingabe = menü.NutzerEingabe();
            } while (!parser.TryParseBruch(eingabe, out bruchRechts));

            operand = EingabeMap.TryGetValue(eingabeOperator, out var value) ? value : Operand.Ungültig;
            AuswahlRechnung(bruchLinks, bruchRechts, operand);
        }

        public void NutzerEingabe(Bruch bruchLinks)
        {
            var parser = new Parser();
            Bruch bruchRechts;
            Operand operand;
            string eingabe;
            string eingabeOperator;

            do
            {
                menü.Operator();
                eingabeOperator = menü.NutzerEingabe();
            } while (!parser.IstOperator(eingabeOperator));

            do
            {
                menü.Zahl2();
                eingabe = menü.NutzerEingabe();
            } while (!parser.TryParseBruch(eingabe, out bruchRechts));

            operand = EingabeMap.TryGetValue(eingabeOperator, out var value) ? value : Operand.Ungültig;
            AuswahlRechnung(bruchLinks, bruchRechts, operand);
        }

        public void NutzereingabeWeitereAuswahl(Bruch ergebnis)
        {
            Auswahl auswahl;
            menü.AuswahlMenü();
            var eingabe = menü.NutzerEingabe();
            auswahl = AuswahlMap.TryGetValue(eingabe, out var value) ? value : Auswahl.Ungültig;
            WeitereAuswahl(auswahl, ergebnis);
        }

        public void AuswahlRechnung(Bruch zahlLinks, Bruch zahlRechts, Operand aktion)
        {
            Bruch ergebnis = null;
            switch (aktion)
            {
                case Operand.Add:
                    ergebnis = zahlLinks + zahlRechts;
                    break;
                case Operand.Sub:
                    ergebnis = zahlLinks - zahlRechts;
                    break;
                case Operand.Multi:
                    ergebnis = zahlLinks * zahlRechts;
                    break;
                case Operand.Teilen:
                    ergebnis = zahlLinks / zahlRechts;
                    break;
                case Operand.Ungültig:
                    menü.Fehler();
                    break;
                default:
                    throw new Exception($"{aktion} wird nicht unterstützt");
            }

            if(ergebnis == null)
            {
                menü.ErgebnisError();
                NutzerEingabe();
            }           

            menü.AusgabeErgebnis(ergebnis.ToString());
            NutzereingabeWeitereAuswahl(ergebnis);
        }

        public void WeitereAuswahl(Auswahl auswahl, Bruch ergebnis)
        {           
            switch (auswahl)
            {
                case Auswahl.WeiterRechnen:
                    menü.WeitereAuswahlWeiterRechnen(ergebnis);
                    NutzerEingabe(ergebnis);
                    break;
                case Auswahl.AlsBruch:
                    menü.WeitereAuswahlAlsBruch(ergebnis.ToString(), Convert.ToString(ergebnis.UmrechnungInDezimal(ergebnis)));
                    NutzereingabeWeitereAuswahl(ergebnis);
                    break;
                case Auswahl.NeueRechnung:
                    menü.WeitereAuswahlClearConsole();
                    NutzerEingabe();
                    break;
                case Auswahl.AlsDezimal:
                    menü.WeitereAuswahlAlsDezimal(ergebnis.ToString(), Convert.ToString(ergebnis.UmrechnungInDezimal(ergebnis)));
                    NutzereingabeWeitereAuswahl(ergebnis);
                    break;
                case Auswahl.Beenden:
                    return;
                default:
                    menü.WeitereAuswahlClearConsole();
                    NutzereingabeWeitereAuswahl(ergebnis);
                    break;
            }          
        }
    }
}
