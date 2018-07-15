using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

namespace OldPackage
{
    public class PlayerUserInterface : MonoBehaviour
    {

        [SerializeField] Image hpImage;
        [SerializeField] Text moneyText;
        [SerializeField] Text restText;
        [SerializeField] Image normalWeaponImage;
        [SerializeField] Image missileWeaponImage;

        Player2D player;

        public void SetPlayer2D(Player2D player)
        {
            this.player = player;

            // イベント登録
            player.OnHpChanged.Subscribe(SetHp).AddTo(gameObject);
            player.OnMoneyChanged.Subscribe(SetMoney).AddTo(gameObject);
            player.OnRestChanged.Subscribe(SetRest).AddTo(gameObject);
            player.OnAttackTypeChanged.Subscribe(SetAttackType).AddTo(gameObject);

            // UI反映
            SetHp(player.Hp);
            SetMoney(player.Money);
            SetRest(player.Rest);
            SetAttackType(player.AttackType);
        }

        void SetHp(int value)
        {
            hpImage.DOFillAmount((float)value / player.MaxHp, 1f);
        }

        void SetMoney(int value)
        {
            moneyText.text = value + "円";
        }

        void SetRest(int value)
        {
            restText.text = "×" + value;
        }

        void SetAttackType(PlayerAttackType value)
        {
            if (value == PlayerAttackType.Normal)
            {
                normalWeaponImage.enabled = true;
                missileWeaponImage.enabled = false;
            }
            else
            {
                normalWeaponImage.enabled = false;
                missileWeaponImage.enabled = true;
            }
        }

    }
}