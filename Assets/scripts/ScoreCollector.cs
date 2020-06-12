using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreCollector : MonoBehaviour
{
    public float Highest = 0;

    public void UpdateScore(float score) {
        if (score > Highest) Highest = score;
        Debug.Log("Score: " + score + " Highest: " + Highest);
    }
}
