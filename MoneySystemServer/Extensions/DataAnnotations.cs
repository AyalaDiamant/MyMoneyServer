using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DTO
{
    public class TZAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidateTZ(value) ? ValidationResult.Success : new ValidationResult("תעודת זהות לא תקינה");
        }

        public static bool ValidateTZ(object value)
        {
            if (value == null)
                return true;

            string val = value.ToString();

            if (val == null)
                return true;

            if (!System.Text.RegularExpressions.Regex.IsMatch(val, @"^\d{5,9}$"))
                return false;

            if (val.Length < 9)
            {
                while (val.Length < 9)
                {
                    val = '0' + val;
                }
            }

            int mone = 0;
            int incNum;
            for (int i = 0; i < 9; i++)
            {
                incNum = Convert.ToInt32(val[i].ToString());
                incNum *= (i % 2) + 1;
                if (incNum > 9)
                {
                    incNum -= 9;
                }
                mone += incNum;
            }

            if (mone % 10 == 0)
                return true;

            return false;
        }
    }

    public class StartWithAttribute : ValidationAttribute
    {
        public string Text { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            return value.ToString().StartsWith(Text);
        }
    }

    public class DigitsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            return value.ToString().ToCharArray().All(x => char.IsDigit(x));
        }
    }

    public class StrongPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            bool letter = false;
            bool number = false;
            bool tav = false;
            string s = value.ToString();

            if (s == "********")
                return true;

            if (s.Length < 7)
                return false;

            for (int i = 0; i < s.Length; i++)
            {
                if (s.Count(c => (c == s[i])) >= 3)
                    return false;

                if ((s[i] >= 'a' && s[i] <= 'z') || (s[i] >= 'A' && s[i] <= 'Z') || (s[i] >= 'א' && s[i] <= 'ת'))
                {
                    letter = true;
                }
                else if (s[i] >= '0' && s[i] <= '9')
                {
                    number = true;
                }
                else
                {
                    tav = true;
                }
            }

            if (!letter || !number || !tav)
                return false;

            return Test(s);
        }

        public static bool Test(string s)
        {
            char[,] keyBoard = new char[11, 14]{
                { '`', '1', '2', '3', '4', '5', '6', '7', '8', '9','0', '-', '=', ' '},
                { ' ', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o','p', '[', ']',' '},
                { ' ', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', ' ',' ', ' '},
                { ' ', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/', ' ',' ', ' '},
                { ' ' ,'/',' ','ק', 'ר' , 'א' ,'ט' , 'ו' , 'ן' ,'ם' , 'פ', ',' , ' ' , ' ' },
                { ' ' ,'ש','ד','ג', 'כ' , 'ע' ,'י' , 'ח' , 'ל' ,'ך' , 'ף', ',' , ' ' , ' '},
                { ' ' ,'ז','ס','ב', 'ה' , 'נ' ,'מ' , 'צ' , 'ת' ,'ץ' , ' ', ' ' , ' ' , ' '},
                { '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', ' '},
                { ' ', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', '{', '}', '|'},
                { ' ', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L',':', ' ', ' ', ' '},
                { ' ', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '<', '>', '?', ' ', ' ', ' '}
            };


            for (int k = 0; k < s.Length - 2; k++)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 14; j++)
                    {
                        if (keyBoard[i, j] == s[k])
                        {
                            if (j + 2 <= 13 && keyBoard[i, j + 1] == s[k + 1] && keyBoard[i, j + 2] == s[k + 2])
                                return false;

                            else if (j - 2 >= 0 && keyBoard[i, j - 1] == s[k + 1] && keyBoard[i, j - 2] == s[k + 2])
                                return false;

                            else if (j + 1 <= 13 && j - 1 >= 0 && keyBoard[i, j - 1] == s[k + 1] && keyBoard[i, j + 1] == s[k + 2])
                                return false;

                            else
                            {
                                i = 14;
                                break;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
