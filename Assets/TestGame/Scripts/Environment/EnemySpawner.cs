using TestGame.Scripts.Creatures;
using TestGame.Scripts.Interfaces;
using TestGame.Scripts.Model;
using TestGame.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestGame.Scripts.Environment
{
    public class EnemySpawner : MonoBehaviour, IPausable
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _finalPointTransform;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private AnimationCurve _spawnTimeCurve;
        [SerializeField] private float _spawnRange;

        private Timer _timer = new();
        private bool _isActive;

        private void Start()
        {
            _isActive = true;
            SpawnEnemy();
            GameSession.Instance.PauseHandler.Register(this);
        }

        private void Update()
        {
            if (_timer.IsReady && _isActive) SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            Vector3 newEnemyPosition = FindNewEnemyPosition();
            //TODO: Работает только если персонаж двигается слева на право. Нужно бы подумать больше двух секунд, чтобы найти более оптимальное решение.
            if (newEnemyPosition.x >= _finalPointTransform.position.x)
            {
                _isActive = false;
                return;
            }
            Instantiate(_enemyPrefab, newEnemyPosition, Quaternion.identity);
            float randomSpawnTime = _spawnTimeCurve.Evaluate(Random.Range(0f, 1f));
            _timer.Value = randomSpawnTime;
            _timer.Reset();
        }

        private Vector3 FindNewEnemyPosition()
        {
            float newEnemyPositionX = _playerTransform.position.x + _spawnRange;
            float newEnemyPositionY = _playerTransform.position.y;
            float newEnemyPositionZ = _playerTransform.position.z;
            Vector3 newEnemyPosition = new Vector3(newEnemyPositionX, newEnemyPositionY, newEnemyPositionZ);
            return newEnemyPosition;
        }

        public void SetPause(bool isPaused)
        {
            //TODO: По-хорошему нужно улучшить таймер чтобы он запоминал оставшееся время при паузе
            _isActive = !isPaused;
        }

        private void OnDestroy()
        {
            GameSession.Instance.PauseHandler.Unregister(this);
        }
    }
}