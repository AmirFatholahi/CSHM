using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Finance
{
    public static class VerhoffWidget
    {
     
      /// <returns></returns>
        public static string Generate(string constant, string sentence, string amount)
        {
            string fixedAmount = amount.PadLeft(14, '0');
            string date = Calendar.CalenderWidget.ToJalaliDate(DateTime.Now, "/").Replace("/", "").ToString();
            sentence = sentence.Substring(sentence.Length - 5, 5);
            var b1 = GenerateB1(constant, sentence,date, amount);
            var b2 = GenerateB2(constant, sentence,date, amount);
            var identifire = constant + b1 + b2 + sentence + date + fixedAmount;
            return identifire;
        }


        public static bool Validate(string identifierNumber, string constant, string sentence, string amount)
        {
            var iden = Generate(constant, sentence, amount);
            if (iden == identifierNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// تولید شناسه واریز
        /// دیگر در پروژه استفاده نمی شود
        /// </summary>
        /// <param name="group">گروه اجباری یا غیراجباری</param>
        /// <param name="accountNumber">شماره حساب</param>
        /// <param name="bill">صورتحساب</param>
        /// <param name="amount">مبلغ</param>
        //public string GenerateIdentifierBefore(string accountNumber, string date, string amount)
        //{
        //    string fixedAmount = amount.PadLeft(14, '0');
        //    var b1 = GenerateB1("9", accountNumber, date, amount);
        //    var b2 = GenerateB2("9", accountNumber, date, amount);
        //    var identifire = "9" + b1 + b2 + accountNumber + date + fixedAmount;
        //    return identifire;
        //}


        // The multiplication table
        static int[,] _d = {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
            {1, 2, 3, 4, 0, 6, 7, 8, 9, 5},
            {2, 3, 4, 0, 1, 7, 8, 9, 5, 6},
            {3, 4, 0, 1, 2, 8, 9, 5, 6, 7},
            {4, 0, 1, 2, 3, 9, 5, 6, 7, 8},
            {5, 9, 8, 7, 6, 0, 4, 3, 2, 1},
            {6, 5, 9, 8, 7, 1, 0, 4, 3, 2},
            {7, 6, 5, 9, 8, 2, 1, 0, 4, 3},
            {8, 7, 6, 5, 9, 3, 2, 1, 0, 4},
            {9, 8, 7, 6, 5, 4, 3, 2, 1, 0}
        };

        // The permutation table
        static int[,] _p = {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
            {1, 5, 7, 6, 2, 8, 3, 0, 9, 4},
            {5, 8, 0, 3, 7, 9, 6, 1, 4, 2},
            {8, 9, 1, 6, 0, 4, 3, 5, 2, 7},
            {9, 4, 5, 3, 1, 2, 6, 8, 7, 0},
            {4, 2, 8, 6, 5, 7, 3, 9, 0, 1},
            {2, 7, 9, 3, 8, 0, 6, 4, 1, 5},
            {7, 0, 4, 6, 9, 1, 3, 2, 5, 8}
        };

        // The inverse table
        static int[] _inv = { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };

        /// <summary>
        /// Validates that an entered number is Verhoeff compliant.
        /// NB: Make sure the check digit is the last one!
        /// </summary>
        /// <param name="num"></param>
        /// <returns>True if Verhoeff compliant, otherwise false</returns>
        public static bool ValidateVerhoeff(string num)
        {
            int c = 0;
            int[] myArray = StringToReversedIntArray(num);

            for (int i = 0; i < myArray.Length; i++)
            {
                c = _d[c, _p[(i % 8), myArray[i]]];
            }

            return c == 0;

        }

        /// <summary>
        /// For a given number generates a Verhoeff digit
        /// Append this check digit to num
        /// </summary>
        /// <param name="num"></param>
        /// <returns>Verhoeff check digit as string</returns>
        private static string GenerateVerhoeff(string num)
        {
            int c = 0;
            int[] myArray = StringToReversedIntArray(num);

            for (int i = 0; i < myArray.Length; i++)
            {
                c = _d[c, _p[((i + 1) % 8), myArray[i]]];
            }

            return _inv[c].ToString();
        }

        /// <summary>
        /// Converts a string to a reversed integer array.
        /// </summary>
        /// <param name="num"></param>
        /// <returns>Reversed integer array</returns>
        private static int[] StringToReversedIntArray(string num)
        {
            int[] myArray = new int[num.Length];

            for (int i = 0; i < num.Length; i++)
            {
                myArray[i] = int.Parse(num.Substring(i, 1));
            }

            Array.Reverse(myArray);

            return myArray;

        }



        private static string GenerateB2(string a, string c, string d, string amount)
        {
            string strAmount = amount.PadLeft(14, '0');
            string strAmount2 = amount.PadLeft(15, '0');
            string s = Reverse(a + c + d + strAmount) + Reverse(strAmount2);

            return GenerateVerhoeff(s);
        }

        private static string GenerateB1(string a, string c, string d, string amount)
        {
            string strAmount = amount.PadLeft(14, '0');
            string strAmount2 = amount.PadLeft(15, '0');
            string s = a + c + d + strAmount + strAmount2;

            return GenerateVerhoeff(s);
        }
        private static string Reverse(string number)
        {
            char[] charArray = number.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
