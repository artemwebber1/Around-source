using Assets.Scripts.PlayerScripts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI.Text.InfoUI
{
    public sealed class ScoresText : MonoBehaviour
    {
        private Player _player;

        private TextMeshProUGUI _scoresText;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _scoresText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
            => EventManager.onPlayerShot += UpdateScores;

        private void OnDisable()
            => EventManager.onPlayerShot -= UpdateScores;

        private void UpdateScores()
        {
            _scoresText.text = _player.TotalScores.ToString();
        }
    }
}
