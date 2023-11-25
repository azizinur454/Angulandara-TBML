using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int rangeDamage = 5;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);

    Rigidbody2D rb;
    PlayerController player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        rb.velocity = new Vector2((moveSpeed.x * 10) * transform.localScale.x, moveSpeed.y);
    }

    private void Update()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        // kondisi apabila objek terkena hit
        if (damage != null)
        {
            Vector2 KnockbackDirection = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            if (player.isArtifactComplete == true)
            {
                rangeDamage = rangeDamage * 2;
            }

            // objek terkena hit atau damage
            bool gotHit = damage.Hit(rangeDamage, KnockbackDirection);

            if (gotHit)
            {
                Debug.Log(collision.name + "Damage : " + rangeDamage);
                Destroy(gameObject);
            }

        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
