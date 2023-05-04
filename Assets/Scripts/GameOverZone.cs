using System;
using UnityEngine;
using UnityEngine.Localization;

public class GameOverZone : MonoBehaviour
{
    [SerializeField] private LocalizedString _message;

    public event Action<string> GameOver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy) && enemy.IsAlive == true)
        {
            GameOver?.Invoke(_message.GetLocalizedString());
        }
    }
}
