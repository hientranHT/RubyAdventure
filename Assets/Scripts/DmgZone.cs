using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgZone : MonoBehaviour
{
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
        //if (rubyController != null)
        //{
        //    rubyController.ChangeSpeed(-1.0f);
        //}
    }

    
}
