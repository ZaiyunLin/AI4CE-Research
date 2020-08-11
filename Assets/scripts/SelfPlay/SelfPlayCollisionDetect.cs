using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfPlayCollisionDetect : MonoBehaviour
{
    // Start is called before the first frame update
    public SelfPlayGM gm;
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
