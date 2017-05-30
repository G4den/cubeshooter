using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    Color FloorStartColor;
    public GM GameManager;
    Color color;
    Transform target;
    public float speed;

    private void Start()
    {
        ranomizeColor();
        GetComponent<SpriteRenderer>().color = color;
        GameManager = GameObject.Find("GameManager").GetComponent<GM>();
        FloorStartColor = GameManager.color;
        target = GameObject.Find("Player").transform;
    }

    void ranomizeColor()
    {
        int number = Random.Range(0, 3);
        print(number);
        switch (number)
        {
            case 0:
                color = new Color(255, 0, 0); break;
            case 1:
                color = new Color(0, 255, 0); break;
            case 2:
                color = new Color(0, 0, 255); break;
        }
    }

    void Update()
    {
        if(FloorStartColor != GameManager.color)
        {
            Destroy(this.gameObject);
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collided");
        if(collision.tag == "Bullet" && color == GameManager.color)
        {
            GameManager.killCounter--;
            GameManager.bullets++;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        
        if(collision.tag == "Player" && color == GameManager.color)
        {
            GameManager.GameOver = true;
            Destroy(this.gameObject);
        }
    }
}