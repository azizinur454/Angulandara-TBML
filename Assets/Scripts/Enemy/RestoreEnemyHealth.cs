using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreEnemyHealth : MonoBehaviour
{
    Damage damage;
    public GameObject player;
    public bool isPlayerAlive = false;
    public int EnemyHealth = 350;

    private void Awake()
    {
        damage = GetComponent<Damage>();
    }

    private void Update()
    {
        RestoreHealth();
    }

    public void RestoreHealth()
    {
        isPlayerAlive = player.GetComponent<Damage>().IsAlive;

        if(!isPlayerAlive)
        {
            damage.Health = EnemyHealth;
        }
    }
}
