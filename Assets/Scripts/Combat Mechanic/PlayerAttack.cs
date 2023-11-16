using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerController player;

    public int playerAttack = 10;
    public int boostAttack = 20;

    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        // kondisi apabila objek terkena hit
        if (damage != null)
        {
            Vector2 KnockbackDirection = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            if(player.prabuArtifact == true)
            {
                // objek terkena hit atau damage
                bool gotHit = damage.Hit(boostAttack, KnockbackDirection);

                if (gotHit)
                {
                    Debug.Log(collision.name + " Damage : " + boostAttack);
                }
            }

            else
            {
                // objek terkena hit atau damage
                bool gotHit = damage.Hit(playerAttack, KnockbackDirection);

                if (gotHit)
                {
                    Debug.Log(collision.name + " Damage : " + playerAttack);
                }
            }
        }
    }
}
