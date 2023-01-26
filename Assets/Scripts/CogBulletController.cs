using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBulletController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Transform transformRuby;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, transformRuby.position) > 3) 
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force, Transform gettransformRuby)
    {
        rb2d.AddForce(direction * force);
        transformRuby = gettransformRuby;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnDestroy();
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
