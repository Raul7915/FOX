using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COIN : MonoBehaviour
{

    public float coinValue = 1;

    public void OnTriggerEnter2D(Collider2D coin)
    {
        if(coin.gameObject.CompareTag("FOX"))
{
	    Score.instance.ChangeScore(coinValue);
		
        }

    }
    
}
