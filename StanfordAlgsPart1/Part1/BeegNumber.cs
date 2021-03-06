﻿using System;

namespace StanfordAlgs
{
    // WEEK 1 - PROBLEM SET 1

    // A class to store and process very large numbers (larger than a UInt64).
    // Can add and subtract two BeegNumbers together, as well as pad them with zeros.
    // Going negative with either operation will cause chaos! Don't do that.
    // Cannot multiply. Use Karatsuba.cs to do that. TODO: Implement that as part of the base class
    // Division is stupid. We don't do that here.
    public class BeegNumber
    {
        public bool isNeg = false; // This doesn't do anything
        public string String { get; private set; }

        // Two overloaded constructors
        public BeegNumber(string number)
        {
            if (number[0] == '-')
            {
                String = number.Substring(1);
                isNeg = true;
            }
            else
            {
                String = number;
            }
        }

        public BeegNumber(int number)
        {
            if (number < 0)
            {
                isNeg = true;
            }
            String = Math.Abs(number).ToString();
        }

        public int Length()
        {
            return String.Length;
        }

        // Wrapper for AddVal
        public void AddTo(BeegNumber a)
        {
            String = AddVal(this, a).String;
        }

        // Adds this beegNumber with the input BeegNumber, returns a string
        public static BeegNumber AddVal(BeegNumber a, BeegNumber b)
        {
            if (a.isNeg && !b.isNeg) // This does nothing.                    TODO: Make negative numbers work
            {
                return new BeegNumber(SubtractVal(b, a));
            }
            else if (b.isNeg && !a.isNeg)
            {
                return new BeegNumber(SubtractVal(a, b));
            }
            else
            {
                // Walks over each char of the string, adding the input number one place at a time. Adds places if it needs to.
                // Mutates this object.

                // Set up some char[]s of the numbers we're adding, and a variable to handle the carry;
                int carry = 0;
                char[] str = ReverseStringToArray(a);
                char[] add = ReverseStringToArray(b);

                // Loop over the entire number we're trying to add (plus two extra places to handle carrying)
                for (int i = 0; i <= add.Length + 1; i++)
                {
                    // Handle what happens if the target number isn't long enough (doesn't have enough places)
                    if (i >= str.Length)
                    {
                        Array.Resize<char>(ref str, str.Length + 1); // Effectively pushes a '0'
                        str[i] = '0';
                    }

                    int sum;
                    if (i >= add.Length)
                    { // Case for when you only have carried values left - add the carried value with the current
                        if (carry > 0)
                        {
                            sum = carry + (int)char.GetNumericValue(str[i]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else // Case for when you're still on the number you're adding, add all three.
                    {
                        sum = carry + (int)char.GetNumericValue(str[i]) + (int)char.GetNumericValue(add[i]);
                    }

                    // Convert the sum into a char[] - it might be two digits.
                    char[] sumStr = ReverseStringToArray(sum.ToString());

                    // Set the now summed element.
                    str[i] = sumStr[0];

                    // Set the carry variable for the next iteration.
                    if (sumStr.Length > 1) carry = (int)char.GetNumericValue(sumStr[1]);
                    else carry = 0;
                }
                return new BeegNumber(ReverseArrayToString(str));
            }
        }

        // Subtracts the input BeegNumber from this here BeegNumber
        public static string SubtractVal(BeegNumber a, BeegNumber b)
        {
            bool isNeg = a.isNeg;
            int carry = 0;
            char[] str = ReverseStringToArray(a);
            char[] subt = ReverseStringToArray(b);

            //if(subt.Length > str.Length) // || char.GetNumericValue(subt[subt.GetUpperBound(0)]) > char.GetNumericValue(str[str.GetUpperBound(0)])
            //{ // This doesn't totally cover the case for subtracting to get a negative number, but wednesday's tuesday, what can i say?.
            //  //We could figure out how to sign.
            //    throw new ArgumentOutOfRangeException();
            //}

            for (int i = 0; i < str.Length; i++)
            {
                int difference;
                if (i >= subt.Length)
                { // Case for when you only have carried values left - subtract the carried value from the current
                    if (carry > 0)
                    {
                        difference = (int)char.GetNumericValue(str[i]) - carry;
                    }
                    else
                    {
                        break;
                    }
                }
                else // Case for when you're still on the number you're subtracting, minus all three.
                {
                    difference = (int)char.GetNumericValue(str[i]) - (int)char.GetNumericValue(subt[i]) - carry;
                }

                char[] diffStr = ReverseStringToArray(Math.Abs(difference).ToString());
                str[i] = diffStr[0];

                if (difference < 0)
                {
                    carry = 1;
                    str[i] = (10 - Math.Abs(difference)).ToString()[0];
                }
                else
                {
                    str[i] = diffStr[0];
                    carry = 0;
                }
            }
            if (isNeg)
            {
                return '-' + ReverseArrayToString(str);
            }
            else
            {
                return (ReverseArrayToString(str));
            }
        }

        // Wrapper for SubtractVal
        public void SubtractFrom(BeegNumber a)
        {
            string newVal = SubtractVal(this, a);
            if (newVal[0].Equals('-'))
            {
                String = newVal.Substring(1);
                isNeg = true;
            }
            else
            {
                String = newVal;
                isNeg = false;
            }
        }

        public void Pad(int places)
        {
            if (places < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            string zeros = "";
            for (int i = 0; i < places; i++)
            {
                zeros += "0";
            }
            String = String + zeros;
        }

        private static char[] ReverseStringToArray(BeegNumber r)
        {
            // Reverse the number and convert it to a char[].
            // We reverse it because its a lot easier to work up from the 1s place that way.
            char[] arr = r.String.ToCharArray();
            Array.Reverse(arr);
            return arr;
        }

        private static char[] ReverseStringToArray(string r)
        {
            // Overload of last function with a string instead of another BeegNumber
            char[] arr = r.ToCharArray();
            Array.Reverse(arr);
            return arr;
        }

        private static string ReverseArrayToString(char[] arr)
        {
            // Put the array back in readable order
            Array.Reverse(arr);

            // Figure out how many leading zeros there are, and slice the array from that point on
            int startPoint = GetLeadingZerosLocation(arr);
            return new string(arr, startPoint, (arr.Length - startPoint));
        }

        private static int GetLeadingZerosLocation(char[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (!arr[i].Equals('0'))
                {
                    return i;
                }
            }
            return 0;
        }
    }
}