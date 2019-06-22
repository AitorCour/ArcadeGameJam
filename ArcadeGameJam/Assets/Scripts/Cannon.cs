using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public void ShotCannon(Cartridge c)
    {
        c.GetBullet().ShotBullet(transform.position, Vector2.right);
    }
}
