using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("bullet collided");
        if(collision.transform.tag == "SideColl")
        {
            print("bullet collided with site");
            GameObject.Find("GameManager").GetComponent<GM>().GameOver = true;
        }
    }
}
