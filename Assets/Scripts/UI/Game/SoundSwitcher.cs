using UnityEngine;
using UnityEngine.UI;

public class SoundSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;
    [SerializeField] private SoundsContainer _soundsContainer;

    private Image _image;
    private float _volume;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _volume = PlayerPrefs.GetFloat(SoundsContainer.VolumeKey, SoundsContainer.VolumeValue);

        SetImage(_volume);
    }

    public void OnClick()
    {
        if (_volume == 0)
        {
            _volume = SoundsContainer.VolumeValue;
            _soundsContainer?.UnMute();
        }
        else
        {
            _volume = 0;
            _soundsContainer?.Mute();
        }

        SetImage(_volume);
        PlayerPrefs.SetFloat(SoundsContainer.VolumeKey, _volume);
    }

    private void SetImage(float value)
    {
        if (value == 0)
        {
            _image.sprite = _off;
        }
        else
        {
            _image.sprite = _on;
        }
    }
}
