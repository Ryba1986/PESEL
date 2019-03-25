﻿namespace PESEL
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Generator
    {
        #region Public methods

        /// <summary>
        /// Generuje listę wszystkich poprawnych możliwych numerów PESEL dla podanej daty.
        /// </summary>
        /// <param name="dateTime">Data dla której ma być wygenerowany numer PESEL</param>
        /// <returns></returns>
        public IEnumerable<string> Generate(DateTime dateTime)
        {
            var month = 0;

            if (dateTime >= DateTime.Now)
            {
                return null;
            }

            if (dateTime.Year < 1900 && dateTime.Year > 1800)
            {
                month = dateTime.Month + 80;
            }
            else if (dateTime.Year < 1800)
            {
                return null;
            }
            else if (dateTime.Year > 1900 && dateTime.Year < 2000)
            {
                month = dateTime.Month;
            }
            else if (dateTime.Year >= 2000)
            {
                month = dateTime.Month + 20;
            }


            var datePeselString = dateTime.Year.ToString(CultureInfo.InvariantCulture).Substring(2, 2);
            if (month.ToString(CultureInfo.InvariantCulture).Length < 2)
            {
                datePeselString += "0" + month;
            }
            else
            {
                datePeselString += month;
            }

            if (dateTime.Day.ToString(CultureInfo.InvariantCulture).Length < 2)
            {
                datePeselString += "0" + dateTime.Day.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                datePeselString += dateTime.Day.ToString(CultureInfo.InvariantCulture);
            }


            return BuildPeselsFromRandomDigits(datePeselString);

        }

        #endregion

        #region Private methods

        /// <summary>
        /// Oblicza sumę kontrolną numeru pesel na podstawie wag (1, 3, 7, 9, 1, 3, 7, 9, 1, 3)
        /// </summary>
        /// <param name="pesel">PESEL</param>
        /// <returns></returns>
        protected int CalculateControlSum(string pesel)
        {
            var weights = new[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

            var calculator = new CheckSumCalculator();
            var checkSum = calculator.Calculate(weights, pesel.Substring(0, 10));

            return checkSum;
        }

        public void SonarTrigger(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, object obj)
        {
            obj = null;

            if (a > b)
            {
                if (b > c)
                {
                    if (c > d)
                    {
                        obj.GetType().ToString();
                    }
                }
            }
        }

        public void SonarTrigger2(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, object obj)
        {
            obj = null;

            if (a > b)
            {
                if (b > c)
                {
                    if (c > d)
                    {
                        obj.GetType().ToString();
                    }
                }
            }
        }


        public void SonarTrigger3(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, object obj)
        {
            SonarTrigger2(a,b,c,d,e,f,g,h,i,j,obj);
            SonarTrigger3(a, b, c, d, e, f, g, h, i, j, obj);
        }

        private IEnumerable<string> BuildPeselsFromRandomDigits(string peselDateString)
        {
            for (var a = 0; a < 10; a++)
            {
                for (var b = 0; b < 10; b++)
                {
                    for (var c = 0; c < 10; c++)
                    {
                        for (var d = 0; d < 10; d++)
                        {
                            var peselWithoutCrc = $"{peselDateString}{a}{b}{c}{d}";

                            yield return $"{peselWithoutCrc}{CalculateControlSum(peselWithoutCrc)}";
                        }
                    }
                }
            }
        }

        #endregion
    }
}
