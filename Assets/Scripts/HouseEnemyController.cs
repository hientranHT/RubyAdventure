using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseEnemyController : MonoBehaviour
{

    public float maxHealOfHouse;

    public float currentHealofHouse;

    public AudioClip audioHouseHit;

    public GameObject ruby;

    public ParticleSystem houseHit;

    public GameObject winGameLog;

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

    private void Start()
    {
        houseHit.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CogBulletController cogBulletController = collision.GetComponent<CogBulletController>();

        if (cogBulletController != null)
        {
            currentHealofHouse--;
            ruby.GetComponent<RubyController>().PlaySound(audioHouseHit);
            houseHit.Play();
        }
        if (currentHealofHouse == 0) 
        {
            Destroy(gameObject);
            if (maxHealOfHouse == 5)
            {
                winGameLog.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

}
