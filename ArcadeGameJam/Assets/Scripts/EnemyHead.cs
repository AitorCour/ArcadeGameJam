using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    private EnemyBehaviour enemyBe;
    private PlayerBehaviour plBehaviour;
    public bool shooter = true;
    void Start()
    {
        enemyBe = GetComponentInParent<EnemyBehaviour>();

        plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Feet")
        {
            enemyBe.canDoDamage = false;
            enemyBe.Damage(2);
            plBehaviour.EnemyJump();
        }
    }
}
