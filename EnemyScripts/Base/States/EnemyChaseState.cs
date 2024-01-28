using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.EnemyScripts.Base.States
{
    public abstract class EnemyChaseState : EnemyState
    {
        private Vector3 DirectionToPlayer => enemy.Player.transform.position - enemy.transform.position;

        public EnemyChaseState(Enemy enemy) : base(enemy) { }

        public override void Enter()
        {
            base.Enter();
            // play MOVE animation
            enemy.Animator.SetBool("Move", true);
        }

        public override void Update()
        {
            base.Update();

            enemy.MoveEnemy(enemy.Player.transform.position);  // move enemy to target
            enemy.RotateEnemy(DirectionToPlayer);

            if (Vector3.Distance(enemy.Player.transform.position, enemy.transform.position) <= enemy.AttackRange)
            {
                enemy.Speed = 0;
                enemy.Animator.SetBool("Move", false);
                enemy.Animator.SetTrigger("Hit");  // kill player using special animation event
            }
        }
    }
}
