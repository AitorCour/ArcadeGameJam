﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : EnemyBehaviour
{
    public float shootCounter;
    public float shootTime;
    public float distance;
    //public bool playerDetected;
    public LayerMask player;
    private Ecanon canon;
    public float lookSpeed = 50;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        shootCounter = 0;
        canon = GetComponentInChildren<Ecanon>();
        speed = 1;
        enemyLife = 8;
        ChangeRotation();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 direction = transform.TransformDirection(Vector2.left) * distance;
        Gizmos.DrawRay(transform.position, direction);
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Vector2 direction = transform.TransformDirection(Vector2.left);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, player);
        /*if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.CompareTag("Player"))
            {
                playerDetected = true;
                playerSeen = true;
                //Debug.Log("PlayerDetected");
            }
        }
        else playerDetected = false;*/
        if (playerDetected)
        {
            anim.SetBool("Shooting", true);
            if (shootCounter >= shootTime)
            {
                Debug.Log("Shoot");
                
                canon.ShotRotateBullets();
                shootCounter = 0;
            }
            else shootCounter += Time.deltaTime;
            speed = 0;
        }
        else
        {
            Move();
            anim.SetBool("Shooting", false);
        }
        speed = 1;

        Debug.DrawLine(this.transform.position, playerBe.transform.position, Color.blue);

        Vector3 vectorToTarget = playerBe.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        canon.transform.rotation = Quaternion.Slerp(canon.transform.rotation, q, Time.deltaTime * lookSpeed);
        
        //Esto dispara a la derecha
        if (angle < 90 && angle > -90)
        {
            playerDetected = true;
        }
        else playerDetected = false;
    }
    protected override void NoLife()
    {
        RunDeath();
    }
    protected override void ChangeRotation()
    {
        base.ChangeRotation();
        if (waypointIndex == 0)
        {
            /*Quaternion target = Quaternion.Euler(0, 0, 0);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);*/
            canon.negative = true;
        }
        else if (waypointIndex == 1)
        {
            /*Quaternion target = Quaternion.Euler(0, 180, 0);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);*/
            canon.negative = false;
        }
    }
}
