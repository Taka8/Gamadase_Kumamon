using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public enum WeaponType
    {
        Melee,
        Missile
    }

    public class PlayerHealth : MonoBehaviour
    {

        [Range(1, 4)]
        [SerializeField] int playerId = 1;
        [SerializeField] int maxHp = 6;
        [SerializeField] int hp;
        [SerializeField] int money = 100;
        [SerializeField] int rest = 2;
        [SerializeField] WeaponType selectedWeapon = WeaponType.Melee;

    }

}