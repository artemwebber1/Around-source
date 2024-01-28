using Assets.Scripts.UI.Screens;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.PlayerScripts.States
{
    public class PlayerDeadState : PlayerState
    {
        private Timer _timerToDeadScreenActivation;

        public PlayerDeadState(Player player) : base(player)
        {
            _timerToDeadScreenActivation = new Timer();
            _timerToDeadScreenActivation.OnComplete(() =>
            {
                EventManager.OnPlayerKilled();
                _timerToDeadScreenActivation.Break();
            });
        }

        public override void Enter()
        {
            _timerToDeadScreenActivation.SetStart(2);

            player.Animator.speed = 1;
            player.Animator.SetBool("Die", true);
        }

        public override void Update()
        {
            _timerToDeadScreenActivation.Tick();
        }
    }
}
