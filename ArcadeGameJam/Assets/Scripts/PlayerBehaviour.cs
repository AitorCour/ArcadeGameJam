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
    private GameObject[] bulletObj;
    private Bullet[] bullet;

    private bool isGrounded;
    private bool isJumping;
    private bool canMove;
    public bool dead;
    //Save
    public float speed;
    public int life;
    public float jumpForce;
    public bool tripleCannon;
    public bool bounce;
    public int bulletBounces;
    public int cannonsNumber;

    public Transform feetPos;
    public float checkRadius;
    private float jumpCounter;
    public float jumpTime;
    public LayerMask ground;
    private bool movingForward = true;

    private bool canRecieveDamage = true;
    private float timeCounterDamage;
    public float damageTime;
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
        bulletObj = GameObject.FindGameObjectsWithTag("Bullet");
        spritePlayer = GetComponentInChildren<SpriteRenderer>();
        canMove = true;
        dead = false;
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground);

        if(isJumping)
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
            }
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
    }
    // Update is called once per frame
    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
    }
    private void HorizontalMovement()
    {
        currentVelocity.x = speed * axis.x;
        if (!canMove) return;

        if (axis.x == 1)
        {
            anim.SetBool("Walking", true);
            animArm.SetBool("WalkingHead", true);
            movingForward = false;
            ChangeRotation();
        }
        else if (axis.x == -1)
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
        anim.SetTrigger("Jump");
    }
    public void EnemyJump()
    {
        if (!canMove) return;
        isJumping = true;
        jumpCounter = jumpTime/2;
        anim.SetTrigger("Jump");
    }
    public void StopJump()
    {
        isJumping = false;
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
            canRecieveDamage = false;
            if (life <= 0)
            {
                Death();
                life = 0;
            }
        }
    }
    void Death()
    {
        if (!dead)
        {
            Debug.Log("Dead");
            canMove = false;
            arm.SetActive(false);
            anim.SetTrigger("Dead");
            dead = true;
        }
        else return;
    }
    public void CannonType()
    {
        tripleCannon = true;
        cannon.cannons = cannonsNumber;
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
}
