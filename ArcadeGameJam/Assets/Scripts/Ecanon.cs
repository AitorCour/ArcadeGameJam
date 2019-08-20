using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecanon : MonoBehaviour
{
    
    public int maxAmmo;
    public bool negative;
    public GameObject bulletPrefab;
    public Transform ammoTransform;
    public Ebullet[] bullets;
    //private float timeCounter;
    //private bool canShot = false;

    private int currentBullet = 0;
    //public float cooldown;

    //public float timeShot;

    void Start ()
    {
        CreateBullets();
    }

    /*void Update ()
    {
       /* if(!canShot)
        {
            timeCounter += Time.deltaTime;
            if(timeCounter >= cooldown)
            {
                canShot = true;
            }
        }*//*
        if (timeCounter >= timeShot)
        {
            ShotBullet();
        }
        timeCounter += Time.deltaTime;
    }*/

    void CreateBullets()
    {
        bullets = new Ebullet[maxAmmo];

        for(int i = 0; i < maxAmmo; i++)
        {
            Vector2 spawnPos = ammoTransform.position;
            spawnPos.x -= i * 0.2f;
            GameObject b = Instantiate(bulletPrefab, spawnPos, Quaternion.identity, ammoTransform);
            b.name = "Bullet_" + i;
            bullets[i] = b.GetComponent<Ebullet>();
        }

        //canShot = true;
        //timeCounter = 0;
    }

    public void ShotBullet()
    {
        //if(!canShot) return;
        //canShot = false;
        //timeCounter = 0;
        //float zRot = transform.parent.eulerAngles.z;
        if(negative)
        {
            bullets[currentBullet].speed = -3;
        }
        else bullets[currentBullet].speed = 3;

        bullets[currentBullet].ShotBullet(transform.position, Vector2.right);
        currentBullet++;
        if(currentBullet >= maxAmmo) currentBullet = 0;
    }
    public void ShotRotateBullets()
    {
        float zRot = transform.eulerAngles.z;

        bullets[currentBullet].ShotBullet(transform.position, zRot - 90);
        currentBullet++;
        if (currentBullet >= maxAmmo) currentBullet = 0;
    }
    public void Orient_0()
    {
        bullets[currentBullet].speed = -3;
    }
    public void Orient_1()
    {
        bullets[currentBullet].speed = 3;
    }
}
