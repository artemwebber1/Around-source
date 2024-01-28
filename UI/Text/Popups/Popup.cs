using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Pool;
using TMPro;
using UnityEngine;


namespace Assets.Scripts.UI.Text.Popups
{
    public sealed class Popup : PoolObject
    {
        private Transform _cam;

        public TextMeshPro TextComponent => _textComponent;
        private TextMeshPro _textComponent;

        private float _lifeTime;
        private float _fadeAnimationDelay;


        private void Awake()
        {
            _textComponent = GetComponent<TextMeshPro>();
            _cam = Camera.main.transform;
        }

        private void OnEnable()
        {
            _lifeTime = 2f;
            _fadeAnimationDelay = 0.7f;

            transform.LookAt(-(_cam.position - transform.position));
            _textComponent.color = Color.white;
        }

        private void Update()
        {
            _fadeAnimationDelay -= Time.deltaTime;
            if (_fadeAnimationDelay <= 0)
                PlayFadeAnimation();

            transform.LookAt(-(_cam.position - transform.position));
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0)
                ReturnToPool();
        }

        private void PlayFadeAnimation()
        {
            _textComponent.color -= new Color(0, 0, 0, 1) * 2 * Time.deltaTime;
        }

        public void SetText(string text)
        {
            _textComponent.text = text;
        }
    }
}