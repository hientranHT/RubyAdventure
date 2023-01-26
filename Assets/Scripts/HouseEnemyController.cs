using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseEnemyController : MonoBehaviour
{

    public float maxHealOfHouse;

    public float currentHealofHouse;

    //public float timeInvincible = 2.0f;

    //bool isInvincible;

    //float invincibleTimer;

    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CogBulletController cogBulletController = collision.GetComponent<CogBulletController>();

        if (cogBulletController != null)
        {
            currentHealofHouse--;
        }
        if(currentHealofHouse==0)
        {
            Destroy(gameObject);
        }
    }

}
