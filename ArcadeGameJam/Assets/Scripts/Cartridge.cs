using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct CartridgeProperties
{
    public GameObject bulletPrefab;
    public int maxAmmo;
}


public class Cartridge
{
    private Bullet[] bullets;
    private int currentBullet = 0;
    
    public Cartridge(GameObject bulletPrefab, Transform parent, Vector2 origin, int maxAmmo)
    {
        bullets = new Bullet[maxAmmo];
        for(int i = 0; i < maxAmmo; i++)
        {
            //Crear la bala.
            GameObject newBullet = GameObject.Instantiate(bulletPrefab, origin, Quaternion.identity, parent);
            newBullet.name = bulletPrefab.name + "_" + i;
            //Guardar la bala en el array de balas
            bullets[i] = newBullet.GetComponent<Bullet>();

            origin.x -= 0.2f;
        }
    } 

    public Bullet GetBullet()
    {
        Bullet b = bullets[currentBullet];

        currentBullet++;
        if(currentBullet >= bullets.Length) currentBullet = 0;

        return b;
    }

    ~Cartridge()
    {

    }
}
