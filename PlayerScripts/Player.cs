using Assets.Scripts.EnemyScripts.Base;
using Assets.Scripts.PlayerScripts.States;
using Assets.Scripts.StateManagement;
using UnityEngine;


namespace Assets.Scripts.PlayerScripts
{
    public sealed class Player : MonoBehaviour
    {
        #region Fields
        
        public Enemy CurrentEnemy { get; set; }

        public float TotalScores { get; private set; }

        public bool IsImmortal { get; set; }

        #region Moving around

        [Header("Round moving")]
        [SerializeField] private float radius;
        public float Radius => radius;
        public Center Center { get; private set; }

        [SerializeField] private Joystick joystick;
        public Joystick Joystick => joystick;
        public int HorizontalJoystick
        {
            get
            {
                if (Joystick.Horizontal > 0)
                {
                    Animator.SetBool("MoveRight", true);
                    Animator.SetBool("MoveLeft", false);
                }
                else if (Joystick.Horizontal < 0)
                {
                    Animator.SetBool("MoveRight", false);
                    Animator.SetBool("MoveLeft", true);
                }

                return Joystick.HorizontalInt;
            }
        }

        #endregion

        #region Characteristics

        public float Speed
        {
            get => Characteristics.Speed;
            set => Characteristics.Speed = value;
        }

        public float AttackSpeed
        {
            get => Characteristics.AttackSpeed;
            set => Characteristics.AttackSpeed = value;
        }

        public int BulletCount;

        [HideInInspector] public float AttackTime;

        #endregion

        #region Components

        public Animator Animator { get; private set; }
        public Gun Gun { get; private set; }
        public PlayerCharacteristics Characteristics { get; private set; }

        #endregion

        #region States

        public StateMachine StateMachine { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerAttackState AttackState { get; private set; }
        public PlayerDeadState DeadState { get; set; }

        #endregion

        #endregion

        #region Monobehaviour

        private void OnEnable()
        {
            EventManager.onPlayerGotHit += Die;
            EventManager.onEnemyGotHit += UpdateScores;
            EventManager.onPlayerShot += UpdateBalance;
        }

        private void OnDisable()
        {
            EventManager.onPlayerGotHit -= Die;
            EventManager.onEnemyGotHit -= UpdateScores;
            EventManager.onPlayerShot -= UpdateBalance;
        }

        private void Awake()
        {
            CurrentEnemy = FindObjectOfType<Enemy>();
            Animator = GetComponent<Animator>();
            Gun = GetComponentInChildren<Gun>();
            Characteristics = GetComponent<PlayerCharacteristics>();
            Characteristics.Init();
        }

        private void Start()
        {
            Center = new Center(position: Vector3.zero, radius: radius);
            
            StateMachine = new StateMachine();
            AttackState = new PlayerAttackState(this);
            MoveState = new PlayerMoveState(this);
            DeadState = new PlayerDeadState(this);
            
            StateMachine.SetState(MoveState);
        }

        private void Update()
        {
            AttackTime -= Time.deltaTime;
            StateMachine.CurrentState.Update();
        }

        #endregion

        public void LookAtTarget()
        {
            transform.LookAt(CurrentEnemy.transform.position + CurrentEnemy.transform.forward);
        }

        #region Events

        private void Die()
        {
            if (StateMachine.CurrentState != DeadState && !IsImmortal)
            {
                StateMachine.SetState(DeadState);
            }
        }

        private void UpdateBalance()
        {
            float balance = PlayerPrefs.GetFloat("PlayerBalance");
            balance += Gun.MoneyPerHit;  // update player's balance value
            PlayerPrefs.SetFloat("PlayerBalance", balance);  // save balance value
        }

        private void UpdateScores()
        {
            TotalScores += Gun.ScoresPerHit;
        }

        #region AnimationEvents

        public void DamageEnemy()
            => Gun.DamageEnemy();

        #endregion

        #endregion
    }
}
