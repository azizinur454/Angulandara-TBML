using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapuraTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    void Update()
    {
       if (currentTeleporter != null)
       {
            transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleport"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleport"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
