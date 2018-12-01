using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CityNameGenerator {

    public static int maxPräfixCount = 4;

    private static string[] präfixUp = { "Unter", "Über", "Neu", "Bad" };
    private static string[] präfixLow = { "unter", "über", "neu", "bad" };
    private static string[] infixUp = { "Gottes", "Stein", "Fichten", "Hopfen", "Heiligen", "Fürsten", "Eichen", "Buchen" };
    private static string[] infixLow = { "gottes", "stein", "fichten", "hopfen", "heiligen", "fürsten", "eichen", "buchen" };
    private static string[] suffix = { "dorf", "berg", "burg", "kirchen", "hausen", "ried", "zell", "ach", "heim", "stätt", "brunn", "bach" };

    public static string GenerateName()
    {
        string s = "Neu-";
        int präfixCount = (int)Random.Range(0, maxPräfixCount);

        if (präfixCount >= 1)
            s += präfixUp[Random.Range(0, präfixUp.Length)];

        for(int i = 2; i <= präfixCount; i++)
        {
            s += präfixLow[Random.Range(0, präfixLow.Length)];
        }

        if(präfixCount >= 1)
            s += infixLow[Random.Range(0, infixLow.Length)];
        else
            s += infixUp[Random.Range(0, infixUp.Length)];

        s += suffix[Random.Range(0, suffix.Length)];

        return s;
    }

    public static void TestGenerate()
    {
        Debug.Log(GenerateName());
    }
}
