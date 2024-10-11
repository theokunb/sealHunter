using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;

public class Loading : MonoBehaviour
{
    private IEnumerator Start()
    {
        //yield return YandexGamesSdk.Initialize(() =>
        //{

        //});
        yield return null;
        MainMenu.Load();
    }
}
