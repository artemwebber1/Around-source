using UnityEngine;


namespace Assets.Scripts.PlayerScripts.States
{
    public sealed class PlayerAttackState : PlayerState
    {
        private Timer _enterTimeTimer;

        private bool RaycastToEnemy(Ray r) =>
            // Raycast only for two layers: Enemies and Obstacles
            Physics.Raycast(r,
                out RaycastHit hit,
                maxDistance: 100,
                layerMask: LayerMask.GetMask("Enemies", "Obstacles")
                ) &&
            !hit.collider.CompareTag("Enemy");

        public PlayerAttackState(Player player) : base(player)
        {
            _enterTimeTimer = new Timer();
            _enterTimeTimer.OnComplete(() =>
            {
                Vector3 directionToEnemy = (player.CurrentEnemy.transform.position - player.transform.position);

                Ray rayToTarget =
                    new Ray(player.transform.position + Vector3.up * 0.5f, directionToEnemy + Vector3.up * 0.5f);

                if (RaycastToEnemy(rayToTarget))
                    return;

                if (player.AttackTime <= 0)
                {
                    player.Animator.SetTrigger("Shoot");
                    player.AttackTime = player.AttackSpeed;
                }
                player.LookAtTarget();
            });
        }

        public override void Enter()
        {
            // When player enters AttackState, he must wait 0.4 seconds before he can shoot
            _enterTimeTimer.SetStart(0.4f);
        }

        public override void Update()
        {
            base.Update();
            if (player.Joystick.Direction != Vector2.zero)  // if joystick was moved
                player.StateMachine.SetState(player.MoveState);

            if (player.BulletCount <= 0)  // if no bullets, do nothing
                return;

            _enterTimeTimer.Tick();
        }
    }
}
