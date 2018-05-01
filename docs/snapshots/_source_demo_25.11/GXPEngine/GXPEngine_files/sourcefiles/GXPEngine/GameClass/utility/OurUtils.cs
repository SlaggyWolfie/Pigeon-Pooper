using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GXPEngine;

public static class OurUtils
{
    private static int bounce = 1;
    //private static int loop = 1;
    public static bool RandomBool()
    {
        int number = Utils.Random(0, 2);
        if (number == 0) return false;
        else return true;
    }
    
    //Int functions
    public static int ValueCheckerAndAlternator(int checkValue, int firstValue, int secondValue, bool badValue)
    {
        if (checkValue == firstValue) checkValue = secondValue;
        else if (checkValue == secondValue) checkValue = firstValue;
        else if (badValue == true) checkValue = firstValue;
        return checkValue;
    }

    public static int ValueBouncer(int checkValue, int firstValue, int lastValue, int bounce = 1)
    {
        checkValue += bounce;
        if (checkValue >= lastValue) bounce = -bounce;
        if (checkValue <= firstValue) bounce = -bounce;
        return checkValue;
    }

    public static int ValueLooper(int checkvalue, int firstValue, int lastValue, int loop = 1)
    {
        checkvalue += loop;
        if (checkvalue > lastValue) checkvalue = firstValue;
        return checkvalue;
    }

    //Float overloads
    public static float ValueCheckerAndAlternator(float checkValue, float firstValue, float secondValue, bool badValue)
    {
        if (checkValue == firstValue) checkValue = secondValue;
        else if (checkValue == secondValue) checkValue = firstValue;
        else if (badValue == true) checkValue = firstValue;
        return checkValue;
    }

    public static float ValueBouncer(float checkValue, float firstValue, float lastValue)
    {
        checkValue += bounce;
        if (checkValue >= lastValue) bounce = -bounce;
        if (checkValue <= firstValue) bounce = -bounce;
        return checkValue;
    }

    public static float ValueLooper(float checkvalue, float firstValue, float lastValue, float loop = 1)
    {
        checkvalue += loop;
        if (checkvalue > lastValue) checkvalue = firstValue;
        return checkvalue;
    }
}

public static class GetAssets
{
    private static string[] fileRoadArray = Directory.GetFiles(@"assets/roads/", "*.png");
    private static string[] fileVictimArray = Directory.GetFiles(@"assets/victims/", "*.png");
    private static string[] fileCarsArray = Directory.GetFiles(@"assets/victims_cars/", "*.png");
    private static string[] fileMotorBikesArray = Directory.GetFiles(@"assets/victims_motorcyclists/", "*.png");
    //private static string[] classPowerArray = 

    public static string GetRandomRoad()
    {
        return fileRoadArray[Utils.Random(0, fileRoadArray.Length)];
    }
    public static string GetRandomVictim()
    {
        return fileVictimArray[Utils.Random(0, fileVictimArray.Length)];
    }
    public static string GetRandomCar()
    {
        return fileCarsArray[Utils.Random(0, fileCarsArray.Length)];
    }
    public static string GetRandomMotorBike()
    {
        return fileMotorBikesArray[Utils.Random(0, fileMotorBikesArray.Length)];
    }

}
