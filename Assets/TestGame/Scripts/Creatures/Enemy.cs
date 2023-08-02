using TestGame.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestGame.Scripts.Creatures
{
    public class Enemy : Character
    {
        [Header("Attack settings"), Space] 
        [SerializeField] private Collider2D _attackZone;

        [Header("Layer settings"), Space] 
        [SerializeField] private int _minSortingOrder = 60;
        [SerializeField] private int _maxSortingOrder = 90;

        protected override void Start()
        {
            base.Start();

            _skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder =
                Random.Range(_minSortingOrder, _maxSortingOrder);
        }

        protected override void Update()
        {
            base.Update();

            if (_skeletonAnimation.AnimationName != _attackAnimation && _attackZone.IsTouchingLayers(_foeLayerMask))
                Attack();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
                damageable.Hit();
        }

        protected override void Attack()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _attackAnimation, false);
        }

        public override void Hit()
        {
            Destroy(gameObject);
        }
    }
}