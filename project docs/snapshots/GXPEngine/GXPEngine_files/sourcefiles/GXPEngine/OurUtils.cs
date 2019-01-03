using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public static class OurUtils
{
    private static int bounce = 1;
    private static int loop = 1;
    public static bool RandomBool()
    {
        int number = Utils.Random(0, 2);
        if (number == 0) return false;
        else return true;
    }

    public static int ValueCheckerAndAlternator(int checkValue, int firstValue, int secondValue, bool badValue)
    {
        if (checkValue == firstValue) checkValue = secondValue;
        else if (checkValue == secondValue) checkValue = firstValue;
        else if (badValue == true) checkValue = firstValue;
        return checkValue;
    }

    public static int ValueBouncer(int checkValue, int firstValue, int lastValue)
    {
        checkValue += bounce;
        if (checkValue >= lastValue) bounce = -1;
        if (checkValue <= firstValue) bounce = 1;
        return checkValue;
    }

    public static int ValueLooper(int checkvalue, int firstValue, int lastValue)
    {
        checkvalue += loop;
        if (checkvalue > lastValue) checkvalue = firstValue;
        return checkvalue;
    }
}
