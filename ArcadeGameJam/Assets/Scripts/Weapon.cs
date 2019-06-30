using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Cannon cannons;//only one
    public Cartridge[] cartridges;
    public GameObject[] bulletsResources;
    //public Cartridge[] cartridges;
    public Transform ammoTransform;
    public int maxAmmo;
    public int currentCartridge = 0;
    public bool posCart;
    public bool bounce;
    // Start is called before the first frame update
    void Start()
    {
        bulletsResources = Resources.LoadAll<GameObject>("Prefabs/Bullets");

        cartridges = new Cartridge[bulletsResources.Length];

        Vector2 spawnPos = Vector2.zero;
        spawnPos.x = -20;

        for (int i = 0; i < bulletsResources.Length; i++)
        {
            GameObject obj = new GameObject("Cartridge_" + i);
            obj.transform.SetParent(ammoTransform);
            obj.transform.localPosition = spawnPos;

            cartridges[i] = new Cartridge(bulletsResources[i], obj.transform, obj.transform.position, maxAmmo);
            spawnPos.y -= 1;
        }
        //currentCannon = 0;
        cannons = GetComponentInChildren<Cannon>();//only one
    }

    public void ShotWeapon()
    {
        cannons.ShotCannon(cartridges[currentCartridge]);
        //Debug.Log("Shoot Weapon");
    }
    public void ChangeCartridge()
    {
        if(posCart)
        {
            if(!bounce)
            {
                currentCartridge = 0;
            }
            else if(bounce)
            {
                currentCartridge = 2;
            }
        }
        else if(!posCart)
        {
            if (!bounce)
            {
                currentCartridge = 1;
            }
            else if (bounce)
            {
                currentCartridge = 3;
            }
        }
    }
}
