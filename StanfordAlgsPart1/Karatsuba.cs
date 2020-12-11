using System;
using System.Collections.Generic;
using System.Text;

namespace StanfordAlgsPart1
{
    // WEEK 1 - PROBLEM SET 1
    public class Karatsuba
    {
        // Multiplies large numbers as strings using the BeegNumber.
        public static string KaratsubaMultiply(string x, string y)
        {
            if(Math.Min(y.Length, x.Length) == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            // Base Case
            if (y.Length == 1 || x.Length == 1)
            {
                return (Int32.Parse(x) * Int32.Parse(y)).ToString();
            }

            // Split the two inputs into four pieces.
            //IE: 1612 and 2048 into 16, 12, 20, 48.
            BeegNumber a, b, c, d;
            string[] x1, y1;

            int maxLength = Math.Max(x.Length, y.Length);
            if(maxLength %2 == 1)
            {
                if(x[0].Equals('0') && y[0].Equals('0'))
                {
                    maxLength--;
                    x = x.Substring(1);
                    y = y.Substring(1);
                }
                else
                {
                    maxLength++;
                }
            }
            while (x.Length < maxLength)
            {
                x = "0" + x;
            }
            while (y.Length < maxLength)
            {
                y = "0" + y;
            }


            x1 = SplitIntInHalf(x);
            y1 = SplitIntInHalf(y);
            a = new BeegNumber(x1[0]);
            b = new BeegNumber(x1[1]);
            c = new BeegNumber(y1[0]);
            d = new BeegNumber(y1[1]);



            BeegNumber ac = new BeegNumber(KaratsubaMultiply(a.String, c.String));
            BeegNumber bd = new BeegNumber(KaratsubaMultiply(b.String, d.String));

            BeegNumber ab = BeegNumber.AddVal(a, b);
            BeegNumber cd = BeegNumber.AddVal(c, d);
            BeegNumber adbc = new BeegNumber(KaratsubaMultiply(ab.String, cd.String));

            BeegNumber subt = new BeegNumber(BeegNumber.SubtractVal(adbc, bd));
            subt.SubtractFrom(ac);

            ac.Pad(maxLength);
            subt.Pad(maxLength / 2);

            BeegNumber total = BeegNumber.AddVal(subt, bd);
            total.AddTo(ac);
            return (total.String);

        }

        public static string[] SplitIntInHalf(string i)
        {
            string[] AB = new string[2];
            if (i.Length <= 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            int outputSize = (int)Math.Floor((decimal)i.Length / 2);
            AB[0] = i.Substring(0, outputSize);
            AB[1] = i.Substring(outputSize);
            return AB;
        }
    }
}
