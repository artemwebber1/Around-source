using Assets.Scripts.StateManagement;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts.States
{
    public class PlayerMoveState : PlayerState
    {
        private float _angle;

        public PlayerMoveState(Player player) : base(player)
        {
            _angle = Random.Range(0, Mathf.PI * 2);
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            // play IDLE animation
            player.Animator.SetBool("MoveRight", false);
            player.Animator.SetBool("MoveLeft", false);
        }

        public override void Update()
        {
            base.Update();
            // if joystick wasn't touched, stop moving and start attack (go to attack state)
            if (player.Joystick.Direction == Vector2.zero)
                player.StateMachine.SetState(player.AttackState);

            player.LookAtTarget();
            Move();
        }

        private void Move()
        {
            _angle += player.HorizontalJoystick * player.Speed * Time.deltaTime;

            float x = player.Center.Position.x + Mathf.Cos(_angle) * player.Center.Radius;
            float z = player.Center.Position.z + Mathf.Sin(_angle) * player.Center.Radius;
            player.transform.position = new Vector3(x, player.transform.position.y, z);
        }
    }
}
