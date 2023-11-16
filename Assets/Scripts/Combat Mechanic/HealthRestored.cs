using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestored : MonoBehaviour
{
    public int healthRestored = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        if(damage)
        {
            damage.Heal(healthRestored);
            Destroy(gameObject);
        }
    }
}
