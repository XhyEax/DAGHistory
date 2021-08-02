using System;


namespace DAGHistory
{
    class CodeEncoder
    {
        public static string DecimalToArbitrarySystem(ulong decimalNumber, ulong radix)
        {
            if (radix < 2UL || radix > (ulong)((long)"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length))
            {
                throw new ArgumentException("The radix must be >= 2 and <= " + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length.ToString());
            }
            if (decimalNumber == 0UL)
            {
                return "0";
            }
            int num = 63;
            ulong num2 = decimalNumber;
            char[] array = new char[64];
            while (num2 != 0UL)
            {
                ulong num3 = num2 % radix;
                array[num--] = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[(int)num3];
                num2 /= radix;
            }
            return new string(array, num + 1, 64 - num - 1);
        }

        public static bool ArbitraryToDecimalSystem(string number, ulong radix, out ulong result)
        {
            result = 0UL;
            bool result2;
            try
            {
                if (radix < 2UL || radix > (ulong)((long)"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length))
                {
                    throw new ArgumentException("The radix must be >= 2 and <= " + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length.ToString());
                }
                if (string.IsNullOrEmpty(number))
                {
                    result2 = false;
                }
                else
                {
                    number = number.ToUpperInvariant();
                    ulong num = 1UL;
                    for (int i = number.Length - 1; i >= 0; i--)
                    {
                        char value = number[i];
                        int num2 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(value);
                        if (num2 < 0)
                        {
                            return false;
                        }
                        result += (ulong)((long)num2 * (long)num);
                        num *= radix;
                    }
                    result2 = true;
                }
            }
            catch
            {
                result2 = false;
            }
            return result2;
        }

        public static string codeEncode(ulong uid)
        {
            return DecimalToArbitrarySystem(uid, 36);
        }

        public static ulong codeDecode(string code)
        {
            ulong res = 0;
            ArbitraryToDecimalSystem(code.ToUpper(), 36UL, out res);
            return res;
        }
    }
}
