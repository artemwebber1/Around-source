using Assets.Scripts.ItemDropScripts.Base;
using UnityEngine;

namespace Assets.Scripts.ItemDropScripts.Items
{
    /// <summary>
    /// ForcePoint is an object, which adds more scores pre hit if player stays near it
    /// </summary>
    public sealed class ForcePoint : Item
    {
        private bool _inContactWithPlayer;

        #region Monobehaviour
        private void OnEnable()
        {
            EventManager.onPlayerShot += DecreaseScoresPerHit;
            transform.position = _player.Center.GetRandomPointOnCircle(1.2f);
        }

        private void OnDisable()
        {
            EventManager.onPlayerShot -= DecreaseScoresPerHit;
        }

        private void OnTriggerExit(Collider _)
        {
            _inContactWithPlayer = false;
            _player.Gun.ScoresPerHit /= 2;
        }
        #endregion

        protected override void ContactWithPlayer()
        {
            _inContactWithPlayer = true;
            _player.Gun.ScoresPerHit *= 2;
        }

        private void DecreaseScoresPerHit()
        {
            if (!_inContactWithPlayer) return;

            EventManager.OnPlayerTookBonus();

            _player.Gun.ScoresPerHit /= 2;
            Destroy(gameObject);
        }
    }
}
