using Assets.Scripts.EnemyScripts.SpikeScripts;
using UnityEngine;


namespace Assets.Scripts.EnemyScripts.Base.States
{
    public abstract class EnemyAttackState : EnemyState
    {
        protected float AttackSpeed { get; private set; }
        private Timer _attackTime;

        public EnemyAttackState(Enemy enemy, float attackSpeed) : base(enemy)
        {
            AttackSpeed = attackSpeed;

            _attackTime = new Timer();
            _attackTime.OnComplete(() =>
            {
                Attack();  // attack player
                _attackTime.SetStart(AttackSpeed);  // restart timer
            });
        }
        
        public override void Enter()
        {
            _attackTime.SetStart(AttackSpeed);
            enemy.Animator.SetBool("Move", false);
        }

        public override void Update()
        {
            base.Update();
            _attackTime.Tick();
        }

        protected abstract void Attack();
    }
}
