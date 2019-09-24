using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBehaviour
{
    //public float shootCounter;
    //public float shootTime;
    public float distance;
    public float hitDistance;
    public float distanceBetween;
    public float runTime;
    private float runCounter;
    public float detectionTime;
    private float detectionCounter;
    public float noDamageTime;
    private float noDamageCounter;
    public float angle;
    public float radius;
    private bool tired;
    private bool attacking;
    private bool facingNegative;
    private bool attack;
    //public bool playerDetected;
    public LayerMask ground;

    private float angleP;
    private Vector2 directionF;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        speed = 3;
        enemyLife = 5;
        ChangeRotation();
        attack = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, hitDistance);

        Gizmos.color = Color.red;
        if (!facingNegative)
        {
            //left
            directionF = transform.TransformDirection(Vector2.left);
        }
        else if (facingNegative)
        {
            //right
            directionF = transform.TransformDirection(Vector2.right);
        }
        Vector2 direction = directionF * 1;
        Gizmos.DrawRay(transform.position - new Vector3(0, 0, 0), direction);

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
        if (!facingNegative)
        {
            //left
            directionF = transform.TransformDirection(Vector2.left);
        }
        else if (facingNegative)
        {
            //right
            directionF = transform.TransformDirection(Vector2.right);
        }
        Vector2 direction = directionF;
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 0, 0), direction, 1, ground);
        if (hit.collider != null)
        {
            ChangeRotation();
        }
        else Move();

        Vector3 vectorToTarget = playerBe.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angleP = angle;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        float distance = Vector2.Distance(this.transform.position, playerBe.transform.position);


        Vector3 fov = Quaternion.AngleAxis(angle, transform.forward) * transform.right;
        RaycastHit2D hitP = Physics2D.Raycast(transform.position, fov, distance);

        if (hitP.collider != null)
        {
            if (hitP.collider.CompareTag("Player"))
            {
                if (angle < 50 && angle > -50 && facingNegative && distance < hitDistance)
                {
                    playerDetected = true;
                    attack = true;
                }
                else if (angle > 130 && angle < 230 && !facingNegative && distance < hitDistance)
                {
                    playerDetected = true;
                    attack = true;
                }
                else playerDetected = false;
                Debug.Log("meleeDetected");
            }
        }
        else if (hitP.collider.CompareTag("ground"))
        {
            playerDetected = false;
        }
        else if (distance > hitDistance)
        {
            playerDetected = false;
        }
        else
        {
            if (detectionCounter >= detectionTime)
            {
                playerDetected = false;
                detectionCounter = 0;
            }
            else detectionCounter += Time.deltaTime;
        }

        if (attack && !attacking && !tired)
        {
            /*if (shootCounter >= shootTime)
            {
                Debug.Log("Shoot");
                canon.ShotBullet();
                shootCounter = 0;
            }*/
            //else shootCounter += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerBe.transform.position.x,
                /*se quita el transform .y del player aquí*/transform.position.y), speed * Time.deltaTime);

            distanceBetween = Vector2.Distance(playerBe.transform.position, transform.position);

            if (distanceBetween <= hitDistance && !attacking && !tired)
            {
                runCounter = 0;
                speed = 5;
                attacking = true;
                anim.SetBool("Attack", true);
            }
            if (attacking && !tired)
            {
                if (runCounter >= runTime)
                {
                    runCounter = 0;
                    speed = 0;
                    tired = true;
                    attacking = false;
                    anim.SetBool("Attack", false);
                }
                runCounter += Time.deltaTime;
            }
            if (tired)
            {
                if (runCounter >= runTime)
                {
                    speed = 2;
                    tired = false;
                    attack = false;
                }
                runCounter += Time.deltaTime;
                runCounter = 0;
                speed = 0;
            }
        }

        if(!canDoDamage)
        {
            if (noDamageCounter >= noDamageTime)
            {
                canDoDamage = true;
                noDamageCounter = 0;

            }
            else noDamageCounter += Time.deltaTime;
        }
    }
    protected override void ChangeRotation()
    {
        base.ChangeRotation();
        if (currentSpeed.x == 1)
        {
            //Right
            facingNegative = true;
        }
        else if (currentSpeed.x == -1)
        {
            //Left
            facingNegative = false;
        }
    }
}
