using Assets.Scripts.PlayerScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Text.InfoUI
{
    public sealed class BulletsCountInfo : MonoBehaviour
    {
        private TextMeshProUGUI _bulletsCountText;
        private Player _player;

        #region Monobehaviour

        private void Awake()
        {
            _player = FindAnyObjectByType<Player>();
            _bulletsCountText = GetComponent<TextMeshProUGUI>();

            UpdateText();
        }

        private void OnEnable()
        {
            EventManager.onPlayerShot += UpdateText;
            EventManager.onPlayerTookAmmo += UpdateText;
        }

        private void OnDisable()
        {
            EventManager.onPlayerShot -= UpdateText;
            EventManager.onPlayerTookAmmo -= UpdateText;
        }

        #endregion

        private void UpdateText() => _bulletsCountText.text = _player.BulletCount.ToString();
    }
}
