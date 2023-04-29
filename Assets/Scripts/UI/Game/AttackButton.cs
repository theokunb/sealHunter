using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerShoot _shootPlayer;

    private float _shootValue = 0;

    private void Update()
    {
        _shootPlayer.Shoot(_shootValue);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _shootValue = 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _shootValue = 0;
    }
}
