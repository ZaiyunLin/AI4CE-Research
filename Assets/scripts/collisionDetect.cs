using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetect : MonoBehaviour
{
   // public gameManager gm;
    public GameManagerCamera gm;
    private void Start()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "shape")
        {

            gm.reset = true;
        }
    }
}
