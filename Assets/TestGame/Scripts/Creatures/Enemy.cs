using System;
using Spine;
using TestGame.Scripts.Interfaces;
using UnityEngine;
using Event = Spine.Event;

namespace TestGame.Scripts.Creatures
{
    public class Enemy : Character
    {
        [Header("Attack settings")] 
        [SerializeField] private Collider2D _attackZone;
        protected override void HandleAnimationState(TrackEntry trackentry, Event e)
        {
            Debug.Log($"Enemy animation get {e.Data.Name} state");
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