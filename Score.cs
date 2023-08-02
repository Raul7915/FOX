
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;
  
    public TextMeshProUGUI text;
    float score;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(float coinValue)
    {
        score += coinValue;

        text.text = "x" + score.ToString();
      

    }
    
}
