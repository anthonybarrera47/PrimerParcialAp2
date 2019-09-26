using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimerParcialAp2.Extensores
{
    public static class Extensores
    {
        public static int ToInt(this string entero)
        {
            int.TryParse(entero, out int valor);
            return valor;
        }
        public static decimal ToDecimal(this string decimales)
        {
            Decimal.TryParse(decimales, out decimal valor);
            return valor;
        }
    }
}