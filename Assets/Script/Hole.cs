using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    //              int point
    private void OnTriggerEnter(Collider other)
    {
        Ball b =other.gameObject.GetComponent<Ball>();

        if (b != null)
        {
            GameManager.instance.PlayerScore += b.Point;
            GameManager.instance.UpdateScoreText();
            Destroy(b.gameObject);
            
        }
    }
}
