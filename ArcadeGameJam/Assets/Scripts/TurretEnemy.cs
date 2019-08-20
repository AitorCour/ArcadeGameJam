using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : EnemyBehaviour
{
    public float shootCounter;
    public float shootDistance;
    public float shootTime;
    private Ecanon cannon;
    //public Transform cannonTrans;
    private Quaternion lookAt;
    private Quaternion targetRotation;
    public float lookSpeed = 50;
    //private Ecanon canon;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        shootCounter = 0;
        cannon = GetComponentInChildren<Ecanon>();

        speed = 1;
        enemyLife = 5;
    }
    // Update is called once per frame
    protected override void Update()
    {
        //si el player esta en su rango, dispara
        base.Update();
        float distance = Vector2.Distance(this.transform.position, playerBe.transform.position);
        if (distance < shootDistance)
        {
            playerDetected = true;
        }
        else if(distance > shootDistance)
        {
            playerDetected = false;
        }
        if(playerDetected)
        {
            if (shootCounter >= shootTime)
            {
                Debug.Log("Shoot");

                cannon.ShotRotateBullets();
                shootCounter = 0;
            }
            else shootCounter += Time.deltaTime;
        }
        Debug.DrawLine(this.transform.position, playerBe.transform.position, Color.blue);

        Vector3 vectorToTarget = playerBe.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        cannon.transform.rotation = Quaternion.Slerp(cannon.transform.rotation, q, Time.deltaTime * lookSpeed);
    }
}
