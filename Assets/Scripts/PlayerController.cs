using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {

    public GM GameManager;
    public Animator muzzleFlash;
    public Transform barrelTransform;
    public int bulletspeed;
    public GameObject BulletPrefab;
    public Text AmmoText;

	// Update is called once per frame
	void Update () {
        AmmoText.text = GameManager.bullets.ToString();

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            Shoot(0,new Vector2(0, 10));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            Shoot(180, new Vector2(0, -10));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            Shoot(90, new Vector2(-10, 0));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
            Shoot(90, new Vector2(10, 0));
        }
    }

    void Shoot(int rotation, Vector2 bulletforce)
    {
        if (GameManager.GameOver != true)
        {
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
