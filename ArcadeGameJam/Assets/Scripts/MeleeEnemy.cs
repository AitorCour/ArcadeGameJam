using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
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
    public bool playerDetected;
    public bool canDoDamage;
    private Ecanon canon;
    private PlayerBehaviour playerBe;
    public LayerMask player;
    private EnemyHead head;
    public Transform[] waypoints;
    public int waypointIndex = 0;
    public float speed;
    public bool testing;
    public int enemyLife;
    // Start is called before the first frame update
    void Start()
    {
        //shootCounter = 0;
        canon = GetComponentInChildren<Ecanon>();
        playerBe = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        head = GetComponentInChildren<EnemyHead>();
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 direction = transform.TransformDirection(Vector2.left) * distance;
        /*if(!isInFOV)
        {
            Debug.DrawLine(transform.position, direction, Color.red);
        }
        else if (isInFOV)
        {
            Debug.DrawLine(transform.position, direction, Color.green);
        }
        Gizmos.DrawWireSphere(transform.position, radius);
        Vector3 fovLine1 = Quaternion.AngleAxis(angle, transform.forward) * -transform.right * radius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-angle, transform.forward) * -transform.right * radius;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
        if(playerBe != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, (playerBe.transform.position - transform.position).normalized * radius);
        }*/
        //Vector3 fovLine1 = Quaternion.AngleAxis(angle, transform.forward) * -transform.right * radius;
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
    void Update()
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
            Move();
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
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               speed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
                ChangeRotation();
            }

            if(transform.position.x < waypoints[0].transform.position.x)
            {
                Quaternion target = Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            }
            if (transform.position.x > waypoints[1].transform.position.x)
            {
                Quaternion target = Quaternion.Euler(0, 0, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            }
        }
        //Repeat
        else if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
            ChangeRotation();
        }
    }
    void ChangeRotation()
    {
        if (waypointIndex == 0)
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            canon.negative = true;
        }
        else if (waypointIndex == 1)
        {
            Quaternion target = Quaternion.Euler(0, 180, 0);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            canon.negative = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canDoDamage)
        {
            playerBe.Damage(1);
            //Debug.Log("ENEMYCOLLISION");
        }
    }
    public void Damage(int damage)
    {
        enemyLife -= damage;
        if (enemyLife <= 0)
        {
            Death();
            enemyLife = 0;
        }
    }
    void Normal()
    {
        Debug.Log("Normal");
        runCounter = 0;
        speed = 2;
    }
    void Death()
    {
        this.enabled = false;
        head.enabled = false;
        transform.position = new Vector2(-20, -20);
    }
}
