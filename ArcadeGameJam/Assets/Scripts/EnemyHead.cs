using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    private ShootEnemy enemy;
    private PlayerBehaviour plBehaviour;
    void Start()
    {
        enemy = GetComponentInParent<ShootEnemy>();
        plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Feet")
        {
            enemy.Damage(2);
            plBehaviour.EnemyJump();
        }
    }
}
