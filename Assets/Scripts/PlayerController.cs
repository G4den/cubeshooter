using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {

    AudioSource AudioS;
    public AudioClip GunSound;
    public GM GameManager;
    public Animator muzzleFlash;
    public Transform barrelTransform;
    public int bulletspeed;
    public GameObject BulletPrefab;

    private void Start()
    {
        AudioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            Shoot(0,new Vector2(0, 10));
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            Shoot(180, new Vector2(0, -10));
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            Shoot(90, new Vector2(-10, 0));
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
            Shoot(90, new Vector2(10, 0));
        }
    }

    void Shoot(int rotation, Vector2 bulletforce)
    {
        if (GameManager.GameOver != true && GameManager.ColorChanged == false)
        {
            AudioS.PlayOneShot(GunSound);
            Camera.main.GetComponent<PerlinShake>().PlayShake();
            GameManager.bullets--;
            muzzleFlash.Play("MuzzleFlash");
            GameObject bullet;
            bullet = Instantiate(BulletPrefab) as GameObject;
            bullet.transform.position = barrelTransform.position;
            bullet.transform.eulerAngles = new Vector3(0, 0, rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletforce * bulletspeed;
        }
    }
}
