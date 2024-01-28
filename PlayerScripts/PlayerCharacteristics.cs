using Assets.Scripts.UI.MainMenuEntities.ShopScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts.States
{
    public class PlayerCharacteristics : MonoBehaviour
    {
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }
        [SerializeField] private float _speed;

        public float AttackSpeed
        {
            get => _attackSpeed;
            set => _attackSpeed = value;
        }
        [SerializeField] private float _attackSpeed;


        public void Init()
        {
            if (!PlayerPrefs.HasKey("PlayerSpeed"))
                PlayerPrefs.SetFloat("PlayerSpeed", _speed);

            if (!PlayerPrefs.HasKey("PlayerAttackSpeed"))
                PlayerPrefs.SetFloat("PlayerAttackSpeed", _attackSpeed);


            _speed = PlayerPrefs.GetFloat("PlayerSpeed");
            _attackSpeed = PlayerPrefs.GetFloat("PlayerAttackSpeed");
        }
    }
}
