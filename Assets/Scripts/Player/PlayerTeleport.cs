using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    Teleporter teleporter;

    private GameObject currentTeleporter;
    private GameObject currentGapura;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
            }
        }
        else
        {
            if (currentGapura != null)
            {
                transform.position = currentGapura.GetComponent<Teleporter>().GetDestination().position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleport"))
        {
            currentTeleporter = collision.gameObject;
        }
        if (collision.CompareTag("Gapura"))
        {
            currentGapura = collision.gameObject;
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

        else if (collision.CompareTag("Gapura"))
        {
            if (collision.gameObject == currentGapura)
            {
                currentGapura = null;
            }
        }
    }
}
