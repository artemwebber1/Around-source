using Assets.Scripts.PlayerScripts;
using UnityEngine;
using Assets.Scripts.ItemDropScripts.Base;
using Assets.Scripts.ItemDropScripts.Items;
using Assets.Scripts.Pool;


namespace Assets.Scripts.ItemDropScripts
{
    public sealed class ItemDropper : MonoBehaviour
    {
        [Header("Items")]
        [SerializeField] private AmmoBox _ammoBoxPrefab;
        private Pool<AmmoBox> _poolOfAmmoBoxes;
        private Timer _ammoSpawnTimer;
        private int _maxAmmosCount;
        private int _currentAmmosCount;

        [SerializeField] private MoneyItem _moneyPrefab;
        private Pool<MoneyItem> _poolOfMoney;
        private Timer _moneySpawnTimer;
        private int _maxMoneyItemsCount;
        private int _currentMoneyItemsCount;

        [SerializeField] private Item[] _bonuses;

        private Player _player;

        #region Bonus spawning

        private Timer _bonusSpawningDelay;

        private int _maxItemCount;
        private int _currentItemCount;

        #endregion

        #region Monobehaviour
        private void OnEnable()
        {
            EventManager.onPlayerTookBonus += OnPlayerTookItem;
            EventManager.onPlayerTookAmmo += OnPlayerTookAmmo;
            EventManager.onPlayerTookMoney += OnPlayerTookMoney;
        }

        private void OnDisable()
        {
            EventManager.onPlayerTookBonus -= OnPlayerTookItem;
            EventManager.onPlayerTookAmmo -= OnPlayerTookAmmo;
            EventManager.onPlayerTookMoney -= OnPlayerTookMoney;
        }

        private void Awake()
        {
            _maxAmmosCount = 3;
            _currentAmmosCount = 0;
            _poolOfAmmoBoxes = new Pool<AmmoBox>(_ammoBoxPrefab, _maxAmmosCount);

            _maxMoneyItemsCount = 5;
            _currentMoneyItemsCount = 0;
            MoneyItem.LoadMoneyRecievedPopup();
            _poolOfMoney = new Pool<MoneyItem>(_moneyPrefab, _maxMoneyItemsCount);

            _maxItemCount = 4;
            _currentItemCount = 0;

            _player = FindObjectOfType<Player>();
            Item.SetPlayer(_player);  // set player's prefab for items
        }

        private void Start()
        {
            // init ammo spawn timer
            _ammoSpawnTimer = new Timer();
            _ammoSpawnTimer.OnComplete(() =>
            {
                SpawnAmmo();
                _ammoSpawnTimer.SetStart(10);
            });

            // init money spawn timer
            _moneySpawnTimer = new Timer();
            _moneySpawnTimer.OnComplete(() =>
            {
                SpawnMoney();
                _moneySpawnTimer.SetStart(20);
            });

            // init other bonuses spawn timer
            _bonusSpawningDelay = new Timer();
            _bonusSpawningDelay.OnComplete(() =>
            {
                SpawnBonus();
                _bonusSpawningDelay.SetStart(15);
            });
        }

        private void Update()
        {
            _bonusSpawningDelay.Tick(_currentItemCount < _maxItemCount);
            _ammoSpawnTimer.Tick(_currentAmmosCount < _maxAmmosCount);
            _moneySpawnTimer.Tick(_currentMoneyItemsCount < _maxMoneyItemsCount);
        }
        #endregion

        #region Objects spawn

        private void SpawnAmmo()
        {
            AmmoBox ammo = _poolOfAmmoBoxes.GetObject();
            ammo.transform.position = _player.Center.GetRandomPointOnCircle(posY: 1.5f);
            _currentAmmosCount++;
        }
        private void OnPlayerTookAmmo() => _currentAmmosCount--;


        private void SpawnMoney()
        {
            MoneyItem money = _poolOfMoney.GetObject();
            money.transform.position = _player.Center.GetRandomPointOnCircle(posY: 2f);
            _currentMoneyItemsCount++;
        }
        private void OnPlayerTookMoney() => _currentMoneyItemsCount--;


        private void SpawnBonus()
        {
            Instantiate(_bonuses[Random.Range(0, _bonuses.Length)]);
            _currentItemCount++;
        }
        private void OnPlayerTookItem() => _currentItemCount--;

        #endregion
    }
}
