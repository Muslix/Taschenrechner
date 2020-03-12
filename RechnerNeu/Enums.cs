using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerNeu
{
    public enum Operand
    {
        Add,
        Sub,
        Multi,
        Teilen,
        Bruch,
        Beenden,
        Ungültig
    }

    public enum Auswahl
    {
        WeiterRechnen,
        AlsBruch,
        AlsDezimal,
        NeueRechnung,
        Beenden,
        Ungültig
    }
}
