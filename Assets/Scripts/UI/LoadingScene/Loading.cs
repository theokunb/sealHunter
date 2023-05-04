using Agava.YandexGames;
using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;

public class Loading : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(() =>
        {
            MainMenu.Load();
        });
    }
}
