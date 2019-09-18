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
    //public bool playerDetected;
    public LayerMask player;
    public bool testing;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        speed = 3;
        enemyLife = 5;
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 direction = transform.TransformDirection(Vector2.left) * distance;
        Vector3 fovLine2 = Quaternion.AngleAxis(-angle, transform.forward) * -transform.right * radius;
        Vector3 fovLine1 = Quaternion.AngleAxis(-angle / 2, transform.forward) * -transform.right * radius;
        //Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, direction);
    }
    /*public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[100];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(-checkingObject.right, directionBetween);

                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius))
                        {
                            if (hit.transform == target)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }*/
    // Update is called once per frame
    protected override void Update()
    {
        Vector2 direction = transform.TransformDirection(Vector2.left);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, player);
        Vector3 fovLine2 = Quaternion.AngleAxis(-angle, transform.forward) * -transform.right;
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, fovLine2, radius, player);
        Vector3 fovLine1 = Quaternion.AngleAxis(-angle/2, transform.forward) * -transform.right;
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, fovLine1, radius, player);

        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.CompareTag("Player"))
            {
                playerDetected = true;
                //Debug.Log("PlayerDetected");
            }
        }
        else if (hit2.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
            if (hit2.collider.CompareTag("Player"))
            {
                playerDetected = true;
                //Debug.Log("PlayerDetected");
            }
        }
        else if (hit1.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
            if (hit1.collider.CompareTag("Player"))
            {
                playerDetected = true;
                //Debug.Log("PlayerDetected");
            }
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

        if (playerDetected && !testing)
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
            }
            if (attacking && !tired)
            {
                if (runCounter >= runTime)
                {
                    runCounter = 0;
                    speed = 0;
                    tired = true;
                    attacking = false;
                }
                runCounter += Time.deltaTime;
            }
            if (tired)
            {
                if (runCounter >= runTime)
                {
                    Normal();
                    speed = 2;
                    tired = false;
                }
                runCounter += Time.deltaTime;
            }
        }
        else
        {
            speed = 2;
            attacking = false;
            runCounter = 0;
            //tired = false;
            //Move();
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
    
    void Normal()
    {
        Debug.Log("Normal");
        runCounter = 0;
        speed = 2;
    }
}
