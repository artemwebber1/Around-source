using Assets.Scripts.StateManagement;


namespace Assets.Scripts.EnemyScripts.Base.States
{
    public class EnemyState : State
    {
        protected readonly Enemy enemy;

        public EnemyState(Enemy enemy)
        {
            this.enemy = enemy;
        }
    }
}
