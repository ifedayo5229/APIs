using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.Helper
{
    public static class Extensions
    {
        public static string FormatAmount(this double amount)
        {
            var separator = ',';
            var minus = "-";
            var newAmount = amount.ToString();
            var newAmountDecimal = "";
            if (newAmount.Contains("."))
            {
                newAmountDecimal = newAmount.Split('.', StringSplitOptions.None).Last();
                newAmount = newAmount.Split('.', StringSplitOptions.None).First();
            }

            var res = "";
            int count = 1;
            for (int i = 1; i <= newAmount.Length; i++)
            {
                int j = newAmount.Length - i;
                res = String.Concat(newAmount[j], res);
                if (i == (3 * count))
                {
                    if (j != 0)
                    {
                        res = String.Concat(separator, res);
                        count++;
                    }
                }
            }
            if (res.Contains(minus))
            {
                var newString = res.Split('-', StringSplitOptions.None).Last();
                if (newString[0] == separator)
                {
                    var finalString = newString.Remove(0, 1);
                    res = string.Concat(minus, finalString);
                    return res;
                }

                if (string.IsNullOrEmpty(newAmountDecimal))
                    return newString;
            }
            if (string.IsNullOrEmpty(newAmountDecimal))
                return res;
            res = String.Concat(res, ".", newAmountDecimal);
            return res;
        }
    }
}
