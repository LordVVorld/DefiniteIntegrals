﻿using System;
using MathNet.Symbolics;

namespace ProgLab1
{
    class SimpsonMethod
    {
        public static int OptimalSplits(double aBord, double bBord, double esp, Expression func)
        {
            double smallerSquare = 1;
            double largerSquare = 0;
            int splits = 4;

            while (Math.Abs(largerSquare - smallerSquare) > esp)
            {
                double splitStep = (bBord - aBord) / splits;
                double x1 = aBord;
                double x2 = aBord;
                double smallerEvenSum = 0;
                double largerEvenSum = 0;
                double smallerOddSum = 0;
                double largerOddSum = 0;

                for (int counter = 1; counter < splits; ++counter)
                {
                    if (counter % 2 == 1)
                    {
                        x1 += splitStep;
                        smallerOddSum += Lab5Form.FuncValue(x1, func);
                    }

                    else
                    {
                        x1 += splitStep;
                        smallerEvenSum += Lab5Form.FuncValue(x1, func);
                    }
                }

                for (int counter = 1; counter < splits * 2; ++counter)
                {
                    if (counter % 2 == 1)
                    {
                        x2 += splitStep / 2;
                        largerOddSum += Lab5Form.FuncValue(x2, func);
                    }

                    else
                    {
                        x2 += splitStep / 2;
                        largerEvenSum += Lab5Form.FuncValue(x2, func);
                    }
                }
                smallerSquare = (splitStep / 3) * (Lab5Form.FuncValue(aBord, func) + 4 * smallerOddSum + 2 * smallerEvenSum + Lab5Form.FuncValue(bBord, func));
                largerSquare = (splitStep / 6) * (Lab5Form.FuncValue(aBord, func) + 4 * largerOddSum + 2 * largerEvenSum + Lab5Form.FuncValue(bBord, func));
                splits *= 2;
            }
            return splits;
        }

        public static double Calculation(double aBord, double bBord, int splits, Expression func)
        {
            double square;
            double sumEven = 0;
            double sumOdd = 0;
            double splitStep = (bBord - aBord) / splits;
            double x = aBord;

            for (int counter = 1; counter < splits; ++counter)
            {
                if (counter % 2 == 1)
                {
                    x += splitStep;
                    sumOdd += Lab5Form.FuncValue(x, func);
                }

                else
                {
                    x += splitStep;
                    sumEven += Lab5Form.FuncValue(x, func);
                }
            }

            square = (splitStep / 3) * (Lab5Form.FuncValue(aBord, func) + 4 * sumOdd + 2 * sumEven + Lab5Form.FuncValue(bBord, func));
            return square;
        }
    }
}
