using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNameGenerator : MonoBehaviour {

    public int maxPräfixCount = 4;

    private string[] präfixUp = { "Unter", "Über", "Neu", "Bad", "Bayerisch" };
    private string[] präfixLow = { "unter", "über", "neu", "bad", "bayerisch" };
    private string[] infixUp = { "Gottes", "Stein", "Fichten", "Hopfen", "Heiligen", "Fürsten", "Eichen", "Buchen" };
    private string[] infixLow = { "gottes", "stein", "fichten", "hopfen", "heiligen", "fürsten", "eichen", "buchen" };
    private string[] suffix = { "dorf", "berg", "burg", "kirchen", "hausen", "ried", "zell", "ach", "heim", "stätt", "brunn", "bach" };

    public string GenerateName()
    {
        string s = "Neu-";
        int präfixCount = (int)Random.Range(0, maxPräfixCount);

        if (präfixCount >= 1)
            s += präfixUp[Random.Range(0, präfixUp.Length - 1)];

        for(int i = 2; i <= präfixCount; i++)
        {
            s += präfixLow[Random.Range(0, präfixLow.Length - 1)];
        }

        if(präfixCount >= 1)
            s += infixLow[Random.Range(0, infixLow.Length - 1)];
        else
            s += infixUp[Random.Range(0, infixUp.Length - 1)];

        s += suffix[Random.Range(0, suffix.Length - 1)];

        return s;
    }

    public void TestGenerate()
    {
        Debug.Log(GenerateName());
    }
}
