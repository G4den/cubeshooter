using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public bool GameOver;

    public Image[] nextColorBar;
    public float enemySpeed;
    public float timeBetweenEnemies;
    public int killCounter;
    public int bullets;
    public Transform[] enemySpawnPos;
    public GameObject Enemy;
    public SpriteRenderer Map;
    public Color color;
    public Color NextColor;
    public Color lerpedColor;

    //public static color lerprgb(color a, color b, float t)
    //{
    //    return new color
    //    (
    //        a.r + (b.r - a.r) * t,
    //        a.g + (b.g - a.g) * t,
    //        a.b + (b.b - a.b) * t,
    //        a.a + (b.a - a.a) * t
    //    );
    //}
    private void Update()
    {
        for (int i = 0; i < nextColorBar.Length; i++)
        {
            nextColorBar[i].fillAmount = Mathf.Lerp(nextColorBar[i].fillAmount, killCounter / 10.0f, Time.deltaTime * 10);
            nextColorBar[i].color = NextColor;
        }
        if (killCounter <= 0)
        {
            color = NextColor;
            Map.color = color;
            killCounter = 10;
            ranomizeColor();
        }

    }

    void Start()
    {
        ranomizeColor();
        color = NextColor;
        Map.color = color;
        killCounter = 10;
        ranomizeColor();
        StartCoroutine(EnemySpawner());
    }

    void ranomizeColor()
    {
        int number = Random.Range(0, 3);
        print(number);
        switch (number)
        {
            case 0:
                NextColor = new Color(255, 0, 0); break;
            case 1:
                NextColor = new Color(0, 255, 0); break;
            case 2:
                NextColor = new Color(0, 0, 255); break;
        }
    }

    IEnumerator EnemySpawner()
    {
        if (!GameOver)
        {
            if (timeBetweenEnemies > 0.2)
            {
                timeBetweenEnemies = timeBetweenEnemies / 1.005f;
            }

            if (enemySpeed < 80)
            {
                enemySpeed = enemySpeed * 1.003f;
            }

            GameObject enemy;
            enemy = Instantiate(Enemy) as GameObject;
            enemy.transform.position = enemySpawnPos[Random.Range(0, 4)].position;
            enemy.GetComponent<Enemy>().speed = enemySpeed;
            yield return new WaitForSeconds(timeBetweenEnemies);
            StartCoroutine(EnemySpawner());
        }
    }
}
