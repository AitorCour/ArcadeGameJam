using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ebullet : MonoBehaviour
{
    
    public float speed;
    public int damage;

    protected bool shot;
    protected Vector2 iniPos;
    protected Vector2 dir;

    public AudioSource shotFX;

    // Use this for initialization
    protected virtual void Start()
    {
        iniPos = transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
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
        ShotBullet(origin, Vector2.down);
        transform.rotation = Quaternion.Euler(0, 0, zRot);
    }

    public virtual void Reset()
    {
        transform.position = iniPos;
        shot = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Boundary")
        {
            Reset();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("UFO COLLISION");

            collision.GetComponent<PlayerBehaviour>().Damage(damage);

            //collision.gameObject.SendMessage("Damage", damage);

            Reset();
        }
    }
}
