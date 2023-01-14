using EShopping.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Core.Domain.ValueObjects
{
    public static class MoneySymbols
    {
        private static Dictionary<MoneyUnit, string> _symbols;

        static MoneySymbols()
        {
            if (_symbols != null)
                return;

            _symbols = new Dictionary<MoneyUnit, string>
            {
                { MoneyUnit.UnSpecified, string.Empty },

                { MoneyUnit.Dollar, "$" },

                { MoneyUnit.Euro, "€" },

                { MoneyUnit.Rial, "Rial" },
            };
        }

        public static string GetSymbol(MoneyUnit moneyUnit)
        {
            return _symbols[moneyUnit].ToString();
        }
    }
}
