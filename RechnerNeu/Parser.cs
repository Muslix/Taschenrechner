using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RechnerNeu
{
    class Parser
    {
        private static readonly Regex PeriodeRegex = new Regex(@"^(?<VorKomma>[+-]?[0-9]+)(,((?<NachKomma>[0-9]+)|(?<NachKomma>[0-9]*)~(?<Periode>[0-9]+)))?$");
        private static readonly Regex BruchRegex = new Regex(@"^(?<Zähler>[+-]?[0-9]+)/(?<Nenner>[+-]?[1-9][0-9]*)$");
        //  private static readonly Regex BruchMitEZählerRegex = new Regex(@"^(?<Zähler>[+-]?[0-9]+)(,(?<ZählerNachKomma>[0-9]+[E]?<ZählerNachE>[0-9]+))/(?<Nenner>[+-]?[0-9]+)(,(?<NennerNachKomma>[0-9]+[E]?<NennerNachE>[0-9]+))$");
        private static readonly Regex OperatorRegex = new Regex(@"^([+]|[-]|[*]|[/]){1}$");
        private static IDictionary<Regex, Func<Match, Bruch>> Regexes { get; } =
            new Dictionary<Regex, Func<Match, Bruch>>()
            {
                { PeriodeRegex, ParsePeriode },
                { BruchRegex, ParseBruch}
            };

        public bool TryParseBruch(string eingabe, out Bruch bruch)
        {
            foreach (var regex in Regexes)
            {
                var match = regex.Key.Match(eingabe);
                if (match.Success)
                {
                    bruch = regex.Value(match);
                    return true;
                }
            }

            bruch = null;
            return false;
        }

        private static Bruch ParsePeriode(Match match)
        {
            var vorKomma = match.Groups["VorKomma"].Value;
            var nachKomma = match.Groups["NachKomma"].Value;
            var periode = match.Groups["Periode"].Value;

            if (!string.IsNullOrEmpty(periode))
            {
                return Bruch.Parse(vorKomma, nachKomma, periode);
            }

            return string.IsNullOrEmpty(periode) && !string.IsNullOrEmpty(nachKomma) ? Bruch.Parse(vorKomma, nachKomma) : Bruch.Parse(vorKomma);
        }

        private static Bruch ParseBruch(Match match)
        {
            var nenner = double.Parse(match.Groups["Nenner"].Value);
            var zähler = double.Parse(match.Groups["Zähler"].Value);

            return Bruch.Parse(zähler, nenner);
        }

        //public static string ParseBruchfürDezimal(string ergebnis)
        //{
        //    var match = BruchRegex.Match(ergebnis);

        //    if (match.Success)
        //    {
        //        var nenner = double.Parse(match.Groups["Nenner"].Value);
        //        var zähler = double.Parse(match.Groups["Zähler"].Value);
        //        return Convert.ToString(nenner / zähler);
        //    }
        //    return null;
        //}

        public bool IstOperator(string @operator)
        {
            return OperatorRegex.IsMatch(@operator);
        }
    }
}
