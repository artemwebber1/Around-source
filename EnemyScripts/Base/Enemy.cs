using Assets.Scripts.EnemyScripts.Base.States;
using Assets.Scripts.PlayerScripts;
using Assets.Scripts.Pool;
using Assets.Scripts.StateManagement;
using Assets.Scripts.UI.Text.Popups;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace Assets.Scripts.EnemyScripts.Base
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Enemy : MonoBehaviour
    {
        #region Abilities
        protected delegate void Ability();  // delegate

        protected HashSet<Ability> _abilities;  // list of enabled abilities

        protected Timer _abilityCastCooldownTimer;  // cooldown timer
        [SerializeField] protected float _abilityCastCooldown;  // cooldown
        #endregion

        #region Round moving

        [Header("Round moving")]
        [SerializeField] private float radius;
        [SerializeField] private Vector3 center;
        [SerializeField] private float rotationSpeed;

        public Center Center { get; private set; }

        #endregion

        #region Characteristics

        [Header("Characteristics")]
        #region Moving characteristics

        [SerializeField] protected float _speed;
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        #endregion

        #region Attack characteristics

        public float AttackSpeed => attackSpeed;
        [SerializeField] private float attackSpeed;

        public float AttackRange => attackRange;
        [SerializeField] private float attackRange;

        #endregion

        #endregion

        #region Components

        public Animator Animator { get; private set; }
        public AudioSource AudioSource { get; private set; }

        #endregion

        #region States

        public StateMachine StateMachine { get; private set; }

        #endregion

        private Popup _popupPrefab;
        private Pool<Popup> _popups;

        public Player Player { get; private set; }
        protected Vector3 DirectionToPlayer => Player.transform.position - transform.position;

        #region MonoBehaviour

        protected virtual void Awake()
        {
            Player = FindObjectOfType<Player>();

            Center = new Center(position: Vector3.zero, radius: radius);
            StateMachine = new StateMachine();

            #region Get components
            Animator = GetComponent<Animator>();
            AudioSource = GetComponent<AudioSource>();
            #endregion
            
            #region Damage poppup initializing

            _popupPrefab = Resources.Load<Popup>("UI/Popups/Popup");
            _popups = new Pool<Popup>(_popupPrefab, 8);

            #endregion

            #region Ability cast initializing
            // init HashSet of abilities
            _abilities = new HashSet<Ability>();

            // create timer for counting cooldown
            _abilityCastCooldownTimer = new Timer();

            _abilityCastCooldownTimer.OnComplete(() =>
            {
                Ability ability = _abilities.ElementAt(Random.Range(0, _abilities.Count));
                ability.Invoke();  // invoke random ability

                // restart timer after ability use
                _abilityCastCooldownTimer.SetStart(_abilityCastCooldown);
            });
            _abilityCastCooldownTimer.SetStart(_abilityCastCooldown);
            #endregion
        }

        protected virtual void Start()
        {
            // set random position
            transform.position = Center.Position + RandomDirection(-Center.Radius / 3, Center.Radius / 3);
        }

        protected virtual void Update()
        {
            _abilityCastCooldownTimer.Tick(_abilities.Count > 0);
        }

        #endregion

        /// <summary>
        /// Animation event of enemy's attack animation
        /// </summary>
        public void KillPlayer()
            => EventManager.OnPlayerGotHit();

        public virtual void TakeDamage()
        {
            Animator.SetTrigger("GetHit");  // play animation
            InstantiateDamagePopup(
                addedScores: Player.Gun.ScoresPerHit, 
                addedMoney: Player.Gun.MoneyPerHit);  // show hit info

            EventManager.OnEnemyGotHit();  // call event [onEnemyGotHit]
        }

        public void MoveEnemy(Vector3 position)
            => transform.position = Vector3.MoveTowards(transform.position, position, Speed * Time.deltaTime);

        #region Enemy rotation
        public void RotateEnemy(Vector3 nextEnemyPosition)
            => transform.rotation = Quaternion.Lerp(transform.rotation,
                GetRotationToNextEnemyPosition(nextEnemyPosition),
                50 * Time.deltaTime);

        public Quaternion GetRotationToNextEnemyPosition(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, angle, 0);
        }
        #endregion

        private void InstantiateDamagePopup(float addedScores, float addedMoney)
        {
            Popup popup = _popups.GetObject();
            popup.SetText($"+{addedScores}");
            popup.transform.position = 
                transform.position +
                Vector3.up * 10 + 
                (Center.Position - transform.position).normalized * 4f + 
                Vector3.right * Random.Range(-2f, 2f);

            Popup moneyRecievedPopup = _popups.GetObject();
            moneyRecievedPopup.SetText($"+{addedMoney}$");
            moneyRecievedPopup.TextComponent.fontSize = 20;
            moneyRecievedPopup.transform.position = popup.transform.position + popup.transform.up * 3;
        }

        private Vector3 RandomDirection(float min, float max)
        {
            float x = Random.Range(min, max);
            float z = Random.Range(min, max);

            return new Vector3(x, transform.position.y, z);
        }
    }
}
