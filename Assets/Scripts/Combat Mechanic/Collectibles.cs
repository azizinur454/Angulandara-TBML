using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectibles : MonoBehaviour
{
    [SerializeField] private int collectibleScores = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Score.instance.AddCoinScore(collectibleScores);
            CharacterEvent.scores(gameObject, collectibleScores);
            Destroy(gameObject);
        }
    }
}
