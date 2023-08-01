using UnityEngine;

namespace TestGame.Scripts.Effects
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float _effectValue;
        [SerializeField] private Transform _followTarget;

        private float _startX;
        private Vector3 _currentPosition;

        private void Start()
        {
            _startX = transform.position.x;
            _currentPosition = transform.position;
            if (_effectValue == 0) _effectValue = Random.Range(.05f, .5f);
        }

        private void LateUpdate()
        {
            var deltaX = _followTarget.position.x * _effectValue;
            transform.position = new Vector3(_startX + deltaX, _currentPosition.y, _currentPosition.z);
        }
    }
}