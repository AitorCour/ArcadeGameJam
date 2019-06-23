using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float iniSpeed;
    public int damage;

    protected bool shot;
    protected Vector2 iniPos;
    protected Vector2 dir;

    public AudioSource shotFX;
    private int collided = 3;
    public bool bounceBullets;
	// Use this for initialization
	protected virtual void Start ()
    {
        iniPos = transform.position;
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        if(shot)
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }        
	}

    public virtual void ShotBullet(Vector2 origin, Vector2 direction)
    {
        shot = true;
        transform.position = origin;
        dir = direction;

        if(shotFX != null)
        {
            shotFX.volume = Random.Range(0.75f, 0.9f);
            shotFX.pitch = Random.Range(0.9f, 1.1f);
            shotFX.Play();
        }
    }

    public virtual void ShotBullet(Vector2 origin, float zRot)
    {
        ShotBullet(origin, Vector2.right);
        transform.rotation = Quaternion.Euler(0, 0, zRot);
    }

    public virtual void Reset()
    {
        transform.position = iniPos;
        shot = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        speed = iniSpeed;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Boundary")
        {
            Reset();
            Debug.Log("bound");
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyShoot")
        {
            Debug.Log("UFO COLLISION");

            collision.GetComponent<ShootEnemy>().Damage(damage);

            //collision.gameObject.SendMessage("Damage", damage);

            Reset();
        }
        else if (bounceBullets && collision.tag != "Player" && collided > 0 && collision.tag != "Bullet" && collision.tag != "Boundary")
        {
            speed *= -1;
            collided -= 1;
        }
        else if (collision.tag != "Boundary" && collision.tag != "Bullet" && collided <= 0)
        {
            Reset();
            Debug.Log("Reseeeeeeeet");
            collided = 4;
            
        }
        else if(!bounceBullets && collision.tag != "Player" && collision.tag != "Bullet" && collision.tag != "Boundary")
        {
            Reset();
            Debug.Log("Reseeeeeeeet Cause Nothing");
        }
        //Debug.Log("collision");
    }
}
