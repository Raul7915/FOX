using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GEMSCORE : MonoBehaviour
{
    public static GEMSCORE instance;

    public TextMeshProUGUI text;
    int score;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void gemChangeScore(int gemValue)
    {
       // gemValue = gemValue / 2;
        score += gemValue;

        text.text = "x" + score.ToString();


    }

}
