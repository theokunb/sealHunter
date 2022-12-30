using UnityEngine;

public class PlayerShop : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    public void OnBuy(PlayerWeapon playerWeapon, Player player)
    {
        if (playerWeapon.CurrentWeapon.IsReloading == true)
        {
            return;
        }

        if (_shop.TryBuyWeapon(out WeaponShop weaponShop, player.Money))
        {
            player.PayWeapon(weaponShop);

            if (playerWeapon.SecondWeapon != null)
            {
                playerWeapon.DropSecondWeapon();
            }

            playerWeapon.TakeNewWeapon(weaponShop);
            playerWeapon.SwitchWeapon();
        }
    }
}
