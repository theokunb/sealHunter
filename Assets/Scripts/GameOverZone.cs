using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    private const string LoosMessage = "Defeat";

    public event Action<string> GameOver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Hipster _))
        {
            return;
        }
        else if (collision.gameObject.TryGetComponent(out Enemy enemy) && enemy.IsAlive == true)
        {
            GameOver?.Invoke(LoosMessage);
        }
    }
}
