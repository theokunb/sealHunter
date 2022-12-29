using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Parallax : MonoBehaviour
{
    [SerializeField] private float _speed;

    private RawImage _image;
    private float _positionX;

    private void Start()
    {
        _image = GetComponent<RawImage>();
    }

    private void Update()
    {
        _positionX += _speed * Time.deltaTime;

        if (_positionX > 1)
        {
            _positionX = 0;
        }

        _image.uvRect = new Rect(_positionX, 0, _image.uvRect.width, _image.uvRect.height);
    }
}
