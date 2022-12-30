using TMPro;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private Transform _weaponPlace;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private TMP_Text _clip;

    public Weapon CurrentWeapon { get; private set; }
    public Weapon SecondWeapon { get; private set; }


    private void Start()
    {
        CurrentWeapon = Instantiate(_defaultWeapon, _weaponPlace);
        Equip(CurrentWeapon);
    }

    public void DropSecondWeapon()
    {
        Destroy(SecondWeapon.gameObject);
    }

    public void TakeNewWeapon(WeaponShop weapon)
    {
        SecondWeapon = Instantiate(weapon.Weapon, _weaponPlace);
    }

    public void OnReload()
    {
        CurrentWeapon.Reload();
    }

    public void SwitchWeapon()
    {
        if (SecondWeapon == null || CurrentWeapon.IsReloading)
        {
            return;
        }

        CurrentWeapon.BulletsInClicpChanged -= OnBulletsCountChanged;
        CurrentWeapon.gameObject.SetActive(false);
        (CurrentWeapon, SecondWeapon) = (SecondWeapon, CurrentWeapon);
        Equip(CurrentWeapon);
    }

    private void Equip(Weapon weapon)
    {
        CurrentWeapon.BulletsInClicpChanged += OnBulletsCountChanged;
        _leftHand.position = CurrentWeapon.LeftHandPlace.position;
        _rightHand.position = CurrentWeapon.RightHandPlace.position;
        CurrentWeapon.gameObject.SetActive(true);
    }

    private void OnBulletsCountChanged(int bullets)
    {
        _clip.text = bullets.ToString();
    }
}
