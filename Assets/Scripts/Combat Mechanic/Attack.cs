using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    public void MagicAttackSound()
    {
        SoundManager.Instance.Play("MagicAttack");
    }

    public void ThunderSound()
    {
        SoundManager.Instance.Play("Thunder");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        // kondisi apabila objek terkena hit
        if(damage != null)
        {
            Vector2 KnockbackDirection = transform.parent.localScale.x > 0 ? knockback : new Vector2 (-knockback.x, knockback.y);

            // objek terkena hit atau damage
            bool gotHit = damage.Hit(attackDamage, KnockbackDirection);

            if (gotHit)
            {
                Debug.Log(collision.name + " Damage : " + attackDamage);
            }
        }
    }
}
