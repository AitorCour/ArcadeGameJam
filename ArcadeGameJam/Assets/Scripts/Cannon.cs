using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public bool tripleCannon = false;
    public void ShotCannon(Cartridge c)
    {
        if(!tripleCannon)
        {
            c.GetBullet().ShotBullet(transform.position, Vector2.right);
        }
        else if(tripleCannon)
        {
            c.GetBullet().ShotBullet(transform.position, 0);
            c.GetBullet().ShotBullet(transform.position, -10);
            c.GetBullet().ShotBullet(transform.position, 10);
        }
    }
}
