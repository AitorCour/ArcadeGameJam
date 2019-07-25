using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : EnemyBehaviour
{
    public float shootCounter;
    public float shootDistance;

    private Quaternion lookAt;
    private Quaternion targetRotation;
    public float lookSpeed = 50;
    //private Ecanon canon;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        shootCounter = 0;
        //canon = GetComponentInChildren<Ecanon>();
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
            Debug.Log("Shoooooooooooot");
        }
        else if(distance > shootDistance)
        {
            playerDetected = false;
        }
        Debug.DrawLine(this.transform.position, playerBe.transform.position, Color.blue);

        Vector3 vectorToTarget = playerBe.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * lookSpeed);
    }
}
