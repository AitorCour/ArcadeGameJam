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
    public LayerMask playerLayer;

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
        Gizmos.color = Color.red;
        Vector2 direction = transform.TransformDirection(Vector2.left) * 1;
        Gizmos.DrawRay(transform.position, direction);
        Gizmos.DrawWireSphere(transform.position, shootDistance);
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

        /*Vector2 direction = transform.TransformDirection(Vector2.left);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, player);
        if (hit.collider != null)
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
        Vector2 direction = transform.TransformDirection(Vector2.left) * 1;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, ground);
        if (hit.collider != null)
        {
            ChangeRotation();
            Debug.Log("ChangeRot");
        }
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

        //Debug.DrawLine(this.transform.position, playerBe.transform.position, Color.blue);

        Vector3 vectorToTarget = playerBe.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angleP = angle;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        canon.transform.rotation = Quaternion.Slerp(canon.transform.rotation, q, Time.deltaTime * lookSpeed);
        float distance = Vector2.Distance(this.transform.position, playerBe.transform.position);


        Vector3 fov = Quaternion.AngleAxis(angle, transform.forward) * transform.right;
        RaycastHit2D hitP = Physics2D.Raycast(transform.position, fov, distance);


        //Esto dispara a la derecha
        if (angle < 90 && angle > -90 && canon.negative && distance < shootDistance)
        {
            if(hitP.collider != null)
            {
                if (hitP.collider.CompareTag("Player"))
                {
                    playerDetected = true;
                    Debug.Log("Right");
                }
                Debug.Log("No null");
            }
            /*playerDetected = true;
            Debug.Log("Right");*/
        }
        else if(angle > 90 && angle < 270 && !canon.negative && distance < shootDistance)
        {
            if (hitP.collider != null)
            {
                if (hitP.collider.CompareTag("Player"))
                {
                    playerDetected = true;
                    Debug.Log("Left");
                }
                Debug.Log("No null");
            }
            //playerDetected = true;
            //Debug.Log("Left");
        }
        /*if (angle > 90 && angle < -90 && canon.negative)
        {
            playerDetected = true;
        }*/
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
