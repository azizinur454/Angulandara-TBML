using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject artifact;

    public void dropArtifact()
    {
        Instantiate(artifact, transform.position, Quaternion.identity);
    }
}
