using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public bool sameAsFloor;
    bool dead;
    public Color FloorStartColor;
    public GM GameManager;
    public Color color;
    Transform target;
    public float speed;
    bool playerColl;
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

        int radomizer = Random.Range(0, 3);
        {
            if(radomizer == 1)
            {
                sameAsFloor = true;
            }
        }
    }

    void Update()
    {
        if (sameAsFloor)
        {
            color = FloorStartColor;
            GetComponent<SpriteRenderer>().color = color;
        }

        if (dead)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 0, Time.deltaTime * 10), Mathf.Lerp(transform.localScale.y, 0, Time.deltaTime * 10), Mathf.Lerp(transform.localScale.z, 0, Time.deltaTime * 10));
        }

        if(FloorStartColor != GameManager.color)
        {
            dead = true;
        }
        if (!dead || playerColl) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collided");
        if(collision.tag == "Bullet" && color == GameManager.color)
        {
            GameManager.killCounter--;
            Destroy(collision.gameObject);
            GameManager.Score += 100;
            dead = true;
        }
        
        if(collision.tag == "Player" && color == GameManager.color && !dead)
        {
            GameManager.GameOver = true;
            dead = true;
        }
        if(collision.tag != "Bullet")
        {
            playerColl = true;
            dead = true;
        }
    }
}