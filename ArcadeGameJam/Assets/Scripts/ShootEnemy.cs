using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public float shootCounter;
    public float shootTime;
    public float distance;
    public bool playerDetected;
    public LayerMask player;
    private Ecanon canon;
    private PlayerBehaviour playerBe;
    private EnemyHead head;
    public Transform[] waypoints;
    public int waypointIndex = 0;
    public float speed;
    private bool movingForward = true;

    public int enemyLife;
    // Start is called before the first frame update
    void Start()
    {
        shootCounter = 0;
        canon = GetComponentInChildren<Ecanon>();
        playerBe = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        head = GetComponentInChildren<EnemyHead>();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 direction = transform.TransformDirection(Vector2.left) * distance;
        Gizmos.DrawRay(transform.position, direction);
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 direction = transform.TransformDirection(Vector2.left);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, player);
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
            playerDetected = true;
        }
        else playerDetected = false;
        if (playerDetected)
        {
            if (shootCounter >= shootTime)
            {
                Debug.Log("Shoot");
                canon.ShotBullet();
                shootCounter = 0;
            }
            else shootCounter += Time.deltaTime;
        }
        else Move();
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

        }
        //Repeat
        else if(waypointIndex >= waypoints.Length)
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
        if(collision.tag == "Player")
        {
            playerBe.Damage(1);
            Debug.Log("ENEMYCOLLISION");
        }
    }
    public void Damage(int damage)
    {
        enemyLife -= damage;
        if(enemyLife <= 0)
        {
            Death();
            enemyLife = 0;
        }
    }
    void Death()
    {
        this.enabled = false;
        head.enabled = false;
        transform.position = new Vector2(-20, -20);
    }
}
