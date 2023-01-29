using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;

    public bool vertical;

    public GameObject ruby;
    public float speed = 1.0f;
    private float distance;
    Rigidbody2D Rigidbody2D;
    bool broken = true;
    public AudioClip audioEnemyHit;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!broken)
        {
            return;
        }

        // Enemy follow Ruby
        distance = Vector2.Distance(transform.position, ruby.transform.position);
        Debug.Log(ruby.transform.position);
        Vector2 vector2 = ruby.transform.position - transform.position;
        if (distance < 3) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, ruby.transform.position, speed * Time.deltaTime);
        }

        // Amimator
        animator.SetFloat("moveX", vector2.x);
        animator.SetFloat("moveY", vector2.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        RubyController rubyController = collision.GetComponent<RubyController>();

        if (rubyController != null)
        {
            if (rubyController.Health <= rubyController.maxHealth)
            {
                rubyController.ChangeHealth(-1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CogBulletController cogBulletController = collision.GetComponent<CogBulletController>();
   
        if (cogBulletController != null)
        {
            cogBulletController.OnDestroy();
            Destroy(gameObject);

            ruby.GetComponent<RubyController>().PlaySound(audioEnemyHit);
        }
    }

    public void Fix()
    {
        broken = false;
        Rigidbody2D.simulated = false;
    }

    public void Launch(Vector2 direction, float force)
    {
        Rigidbody2D.AddForce(direction * force);
    }
}
