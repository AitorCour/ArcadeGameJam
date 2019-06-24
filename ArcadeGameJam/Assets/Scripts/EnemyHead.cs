using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    private ShootEnemy enemy;
    private MeleeEnemy enemyMelee;
    private PlayerBehaviour plBehaviour;
    public bool shooter = true;
    void Start()
    {
        if (shooter)
        {
            enemy = GetComponentInParent<ShootEnemy>();
        }
        else
        {
            enemyMelee = GetComponentInParent<MeleeEnemy>();
        }

        plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Feet")
        {
            if (shooter)
            {
                enemy.Damage(2);
            }
            else
            {
                enemyMelee.Damage(2);
            }
            plBehaviour.EnemyJump();
        }
    }
}
