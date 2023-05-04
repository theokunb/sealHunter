using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Entry : MonoBehaviour
{
    [SerializeField] private TMP_Text _place;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;

    public void Render(string place, string name, string score)
    {
        _place.text = place;
        _name.text = name;
        _score.text = score;
    }
}
