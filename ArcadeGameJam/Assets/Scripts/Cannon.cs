using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    //public bool tripleCannon = false;
    public int bounces;
    public int scale;
    public int cannons;
    private int rotation;
    public void ShotCannon(Cartridge c)
    {
        /*if(!tripleCannon)
        {
            c.GetBullet().ShotBullet(transform.position, Vector2.right, bounces);
        }
        else if(tripleCannon)
        {
            c.GetBullet().ShotBullet(transform.position, 0, bounces);
            c.GetBullet().ShotBullet(transform.position, -10, bounces);
            c.GetBullet().ShotBullet(transform.position, 10, bounces);
        }*/
        for(int i = 0; i < cannons; i++)
        {
            //rotation += i * -5;
            //rotation *= -1;
            rotation = Random.Range( i*-4, i*4);
            c.GetBullet().ShotBullet(transform.position, rotation, bounces, scale);
        }
        rotation = 0;
    }
}
