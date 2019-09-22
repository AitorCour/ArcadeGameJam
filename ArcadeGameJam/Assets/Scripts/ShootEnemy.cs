using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : EnemyBehaviour
{
    public float shootCounter;
    public float shootTime;
    //public float distance;
    public float shootDistance;
    //public bool playerDetected;
    public LayerMask ground;

    private Ecanon canon;
    public float lookSpeed = 50;

    private float angleP;
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
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, shootDistance);

        Gizmos.color = Color.red;
        Vector2 direction = transform.TransformDirection(Vector2.left) * 3;
        Gizmos.DrawRay(transform.position - new Vector3(-1.5f, 0, 0), direction);
        
        if (playerDetected)
        {
            Gizmos.color = Color.green;
        }
        else Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, playerBe.transform.position);

        Gizmos.color = Color.yellow;
        Vector3 fov = Quaternion.AngleAxis(angleP, playerBe.transform.forward) * transform.right * 3;
        Gizmos.DrawRay(transform.position, fov);
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector2 direction = transform.TransformDirection(Vector2.left) * 1;
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(-1.5f, 0, 0), direction, 3, ground);
        if (hit.collider != null)
        {
            ChangeRotation();
        }
        if (playerDetected)
        {
            anim.SetBool("Shooting", true);
            if (shootCounter >= shootTime)
            {
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

        //Debug.DrawLine(this.transform.position, playerBe.transform.position, Color.blue);

        Vector3 vectorToTarget = playerBe.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angleP = angle;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        canon.transform.rotation = Quaternion.Slerp(canon.transform.rotation, q, Time.deltaTime * lookSpeed);
        float distance = Vector2.Distance(this.transform.position, playerBe.transform.position);


        Vector3 fov = Quaternion.AngleAxis(angle, transform.forward) * transform.right;
        RaycastHit2D hitP = Physics2D.Raycast(transform.position, fov, distance);
        
        if (hitP.collider != null)
        {
            if (hitP.collider.CompareTag("Player"))
            {
                if (angle < 50 && angle > -50 && canon.negative && distance < shootDistance)
                {
                    playerDetected = true;
                }
                else if (angle > 130 && angle < 230 && !canon.negative && distance < shootDistance)
                {
                    playerDetected = true;
                }
                else playerDetected = false;
            }
        }
        else if (hitP.collider.CompareTag("ground"))
        {
            playerDetected = false;
        }
        else if(distance > shootDistance)
        {
            playerDetected = false;
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
        if (currentSpeed.x == 1)
        {
            //Right
            canon.negative = true;
        }
        else if (currentSpeed.x == -1)
        {
            //Left
            canon.negative = false;
        }
    }
}
