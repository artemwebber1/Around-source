using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ItemDropScripts.Items.Shield
{
    /// <summary>
    /// Activated shield which defends player
    /// </summary>
    public class ActivatedShield : MonoBehaviour
    {
        [SerializeField, Range(5, 20)] private float _lifeTime;
        [SerializeField, Range(3, 6)] private float _targetScale;
        
        private Timer _lifeTimeTimer;

        private void Start()
        {
            _lifeTimeTimer = new Timer();
            _lifeTimeTimer.OnComplete(() =>
            {
                transform.localScale = Vector3.Lerp(
                    transform.localScale,
                    Vector3.zero,
                    3.5f * Time.deltaTime);

                if (transform.localScale.x <= 1)
                {
                    Destroy(gameObject);
                    _lifeTimeTimer.Break();
                }
            });

            _lifeTimeTimer.SetStart(_lifeTime);
        }

        private void Update()
        {
            _lifeTimeTimer.TickWith(() =>
            {
                if (transform.localScale.x <= _targetScale)
                {
                    transform.localScale = Vector3.Lerp(
                        transform.localScale,
                        Vector3.one * _targetScale,
                        2 * Time.deltaTime);
                }
            });
        }
    }
}
