using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public Animator transistion;
    public bool lastCol;
    public bool ColorChanged;
    public bool GameOver;
    public float Score;
    public float LerpedScore;
    public Text ScoreText;
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
    public bool ShowGameOver;
    public Animator HighscoreObject;
    public bool justStarted;
    public Text GoScore;
    public Text GoHighScore;

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

    IEnumerator RestartGame()
    {
        transistion.Play("RightToLeft");
        yield return new WaitForSeconds(1);
        Application.LoadLevel(0);
    }
    private void Update()
    {
        if (GameOver && ShowGameOver == false)
        {
            ShowGameOver = true;
            if (Score > PlayerPrefs.GetFloat("HS"))
            {
                PlayerPrefs.SetFloat("HS", Score);
            }
            HighscoreObject.Play("In");
            GoScore.text = "Score: " + Score.ToString();
            GoHighScore.text = "Highscore: " + PlayerPrefs.GetFloat("HS");
            ScoreText.GetComponent<Animator>().Play("ScoreTextOut");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RestartGame());
        }

        LerpedScore = Mathf.Lerp(LerpedScore, Score, Time.deltaTime * 10);
        ScoreText.text = LerpedScore.ToString("F0"); 
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
            ColorChanged = true;
            StartCoroutine(JustChangedColor());
        }

    }

    IEnumerator JustChangedColor()
    {
        yield return new WaitForSeconds(.5f);
        ColorChanged = false;
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
                if (color == new Color(255, 0, 0))
                {
                    NextColor = new Color(0, 255, 0); break;
                }
                else{
                    NextColor = new Color(255, 0, 0); break;
                }

            case 1:
                if (color == new Color(0, 255, 0))
                {
                    NextColor = new Color(0, 0, 255); break;
                }
                else
                {
                    NextColor = new Color(0, 255, 0); break;
                }
            case 2:
                if (color == new Color(0, 0, 255))
                {
                    NextColor = new Color(255, 0, 0); break;
                }
                else
                {
                    NextColor = new Color(0, 0,255); break;
                }
        }
    }

    IEnumerator EnemySpawner()
    {
        if (justStarted)
        {
            yield return new WaitForSeconds(2);
            justStarted = false;
        }
        if (!GameOver)
        {
            if (ColorChanged == false)
            {
                if (timeBetweenEnemies > 0.15)
                {
                    timeBetweenEnemies = timeBetweenEnemies / 1.002f;
                }

                if (enemySpeed < 100)
                {
                    enemySpeed = enemySpeed * 1.002f;
                }

                GameObject enemy;
                enemy = Instantiate(Enemy) as GameObject;
                enemy.transform.position = enemySpawnPos[Random.Range(0, 4)].position;
                enemy.GetComponent<Enemy>().speed = enemySpeed;
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
            yield return null;
            StartCoroutine(EnemySpawner());
        }
    }
}
