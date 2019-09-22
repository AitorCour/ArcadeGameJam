using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Vector2 axis;
    private Vector2 currentVelocity;
    private Rigidbody2D rb;
    private SpriteRenderer spritePlayer;
    private SpriteRenderer spriteArm;
    private GameObject arm;
    private Animator anim;
    private Animator animArm;
    private Weapon weapon;
    private Cannon cannon;
    private HUD hud;
    private GameObject[] bulletObj;
    private Bullet[] bullet;

    private bool isGrounded;
    private bool isJumping;
    private bool canMove;
    public bool dead;
    //Save
    public float speed;
    public int life;
    public int iniLife;
    public int maxLife;
    public float jumpForce;
    public bool tripleCannon;
    public bool bounce;
    public int bulletBounces;
    public int cannonsNumber;
    public float bulletsScale;

    public Transform feetPos;
    public float checkRadius;
    private float jumpCounter;
    public float jumpTime;
    public LayerMask ground;
    public LayerMask enemy;
    private bool movingForward = true;

    private bool canRecieveDamage = true;
    private float timeCounterDamage;
    public float damageTime;

    public float distance;
    public float distance2;
    public float heightLow;
    private bool canEat;
    public bool eating;
    public float eatCounter;
    public EnemyBehaviour targetEnemy;
    private Vector2 enemyPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        animArm = GameObject.FindGameObjectWithTag("Arm").GetComponent<Animator>();
        spriteArm = GameObject.FindGameObjectWithTag("Arm").GetComponent<SpriteRenderer>();
        arm = GameObject.FindGameObjectWithTag("Arm");
        weapon = GetComponentInChildren<Weapon>();
        cannon = GetComponentInChildren<Cannon>();
        hud = GameObject.FindGameObjectWithTag("Manager").GetComponent<HUD>();
        bulletObj = GameObject.FindGameObjectsWithTag("Bullet");
        spritePlayer = GetComponentInChildren<SpriteRenderer>();
        canMove = true;
        dead = false;
        canEat = false;
        targetEnemy = null;
        life = iniLife;
        //SetLife();
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground);
        if (isJumping)
        {
            //rb.velocity = Vector2.up * jumpForce;
            jumpCounter -= Time.deltaTime;
            if (jumpCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            else
            {
                isJumping = false;
                jumpCounter = jumpTime;
                animArm.SetTrigger("MidJump");
            }
        }
        if(isGrounded)
        {
            anim.SetBool("Jumping", false);
            animArm.SetBool("Jumping", false);
        }
        else if(!isGrounded)
        {
            anim.SetBool("Jumping", true);
            animArm.SetBool("Jumping", true);
        }
    }
    void Update()
    {
        if(canMove)
        {
            HorizontalMovement();

            transform.Translate(currentVelocity * Time.deltaTime, Space.World);
        }

        if(!canRecieveDamage)
        {
            spritePlayer.color = new Color(spritePlayer.color.r, spritePlayer.color.g, spritePlayer.color.b, 0.5f);
            spriteArm.color = new Color(spriteArm.color.r, spriteArm.color.g, spriteArm.color.b, 0.5f);
            if (timeCounterDamage >= damageTime)
            {
                canRecieveDamage = true;
                spritePlayer.color = new Color(spritePlayer.color.r, spritePlayer.color.g, spritePlayer.color.b, 1f);
                spriteArm.color = new Color(spriteArm.color.r, spriteArm.color.g, spriteArm.color.b, 1f);
                timeCounterDamage = 0;
            }
            else timeCounterDamage += Time.deltaTime;
        }
        if (eating)
        {
            canEat = false;
            //Debug.Log("Loooooop EAT");
            if (eatCounter >= 4)
            {

                eating = false;
                targetEnemy.Death();
                //Subir vida
                Cure(1);
                animArm.SetBool("Eating", false);
            }
            else eatCounter += Time.deltaTime;
        }
        Vector2 direction = transform.TransformDirection(Vector2.left);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, enemy);

        Vector2 direction2 = transform.TransformDirection(Vector2.right);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, direction2, distance2, ground);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0, heightLow, 0), direction2, distance2, ground);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position - new Vector3(0, heightLow, 0), direction2, distance2, ground);


        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyBehaviour target = hit.transform.gameObject.GetComponent<EnemyBehaviour>();
                if (!target.playerSeen)
                {
                    hud.DisplayText("Press Z/B to eat!");
                    canEat = true;
                    targetEnemy = target;
                }
                else
                {
                    hud.HideText();
                    canEat = false;
                    return;
                }
            }
            else
            {
                hud.HideText();
                canEat = false;
                return;
            }
        }
        else
        {
            hud.HideText();
            canEat = false;
            return;
        }

        if(hit1.collider != null /*|| hit2.collider != null || hit3.collider != null*/)
        {
            Debug.Log("cannot move");
        }
    }
    // Update is called once per frame
    private void OnDrawGizmosSelected()
    {
        Vector2 direction = transform.TransformDirection(Vector2.left) * distance;
        Gizmos.DrawRay(transform.position, direction);

        Gizmos.color = Color.red;
        Vector2 direction2 = transform.TransformDirection(Vector2.right) * distance2;
        Gizmos.DrawRay(transform.position, direction2);
        Gizmos.DrawRay(transform.position + new Vector3(0, heightLow,0) , direction2);
        Gizmos.DrawRay(transform.position - new Vector3(0, heightLow, 0), direction2);
    }
    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
    }
    private void HorizontalMovement()
    {
        currentVelocity.x = speed * axis.x;
        //currentVelocity.x = speed * 1;
        if (!canMove) return;

        if (axis.x >= 0.1)
        {
            anim.SetBool("Walking", true);
            animArm.SetBool("WalkingHead", true);
            movingForward = false;
            ChangeRotation();
        }
        else if (axis.x <= -0.1)
        {
            anim.SetBool("Walking", true);
            animArm.SetBool("WalkingHead", true);
            movingForward = true;
            ChangeRotation();
        }
        else
        {
            anim.SetBool("Walking", false);
            animArm.SetBool("WalkingHead", false);
        }
    }
    public void StartJump()
    {
        if (!isGrounded) return;
        else if (isJumping) return;
        else if (!canMove) return;
        isJumping = true;
        jumpCounter = jumpTime;
    }
    public void EnemyJump()
    {
        if (!canMove) return;
        isJumping = true;
        jumpCounter = jumpTime/2;
        //anim.SetTrigger("Jump");
        anim.SetBool("Jumping", true);
    }
    public void StopJump()
    {
        isJumping = false;
       // anim.SetBool("Jumping", false);
    }
    void ChangeRotation()
    {
        if (!movingForward)
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            weapon.posCart = true;
            weapon.ChangeCartridge();
        }
        else if (movingForward)
        {
            Quaternion target = Quaternion.Euler(0, 180, 0);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            weapon.posCart = false;
            weapon.ChangeCartridge();
        }
    }
    public void Damage(int damage)
    {
        if(canRecieveDamage && !dead)
        {
            life -= damage;
            SetLife();
            canRecieveDamage = false;
            if (life <= 0)
            {
                Death();
                life = 0;
            }
        }
        if(eating)
        {
            StopEating();
            targetEnemy.canMove = true;
            targetEnemy.transform.position = transform.position;
        }
    }
    private void Cure(int cure)
    {
        if (life >= maxLife && !dead) return;
        else
        {
            life += cure;
            SetLife();
        }
    }
    public void GetLife()
    {
        maxLife += 2;
        life += 2;
        hud.AddContainers();
        SetLife();
    }
    void Death()
    {
        if (!dead)
        {
            Debug.Log("Dead");
            canMove = false;
            anim.SetTrigger("Dead");
            animArm.SetTrigger("Death");
            dead = true;
        }
        else return;
    }
    public void CannonType()
    {
        tripleCannon = true;
        cannon.cannons = cannonsNumber;
    }
    public void BulletsScale()
    {
        cannon.scale = bulletsScale;
    }

    public void BounceBullet()
    {
        bounce = true;
        weapon.bounce = true;
        cannon.bounces = bulletBounces;
    }
    public void Shoot()
    {
        animArm.SetTrigger("Shoot");
    }
    public void Eat()
    {
        if (!canEat) return;
        Debug.Log("Eating");
        eating = true;
        eatCounter = 0;
        enemyPosition = targetEnemy.transform.position;
        targetEnemy.transform.position = new Vector2(-20, -20);
        targetEnemy.canMove = false;
        animArm.SetBool("Eating", true);
        animArm.SetTrigger("Eat");
    }
    public void StopEating()
    {
        if (targetEnemy.isDead) return;
        eating = false;
        targetEnemy.transform.position = enemyPosition;
        animArm.SetBool("Eating", false);
    }
    private void SetLife()
    {
        Debug.Log("function Called");
        if (hud != null)
        {
            hud.SetLife(life);
        }
        if(hud == null)
        {
            iniLife = life;
            hud = GameObject.FindGameObjectWithTag("Manager").GetComponent<HUD>();
            hud.SetLife(life);
        }
    }
}
