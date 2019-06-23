using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public bool tripleCannon = false;
    public int bounces;
    public void ShotCannon(Cartridge c)
    {
        if(!tripleCannon)
        {
            c.GetBullet().ShotBullet(transform.position, Vector2.right, bounces);
        }
        else if(tripleCannon)
        {
            c.GetBullet().ShotBullet(transform.position, 0, bounces);
            c.GetBullet().ShotBullet(transform.position, -10, bounces);
            c.GetBullet().ShotBullet(transform.position, 10, bounces);
        }
    }
}
