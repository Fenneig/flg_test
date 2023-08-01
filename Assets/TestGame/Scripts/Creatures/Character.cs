using Spine;
using Spine.Unity;
using TestGame.Scripts.Interfaces;
using UnityEngine;
using Event = Spine.Event;

namespace TestGame.Scripts.Creatures
{
    public abstract class Character : MonoBehaviour, IDamageable
    {
        [Header("Animations")]
        [SerializeField] protected SkeletonAnimation _skeletonAnimation;
        [SerializeField] protected string _runAnimation;
        [SerializeField] protected string _attackAnimation;
        [Header("Character settings"), Space] 
        [SerializeField, Range(0, 10)] protected float _speed;
        [SerializeField] protected bool _isRight;
        [Header("Foe layer"), Space]
        [SerializeField] protected LayerMask _foeLayerMask; 


        protected Vector3 MoveDirection => _isRight ? Vector3.right : Vector3.left;
        
        private void Start()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _runAnimation, true);
            _skeletonAnimation.AnimationState.Event += HandleAnimationState;
        }

        protected virtual void Update()
        {
            if (_skeletonAnimation.AnimationName == _runAnimation) Move();
        }

        protected abstract void HandleAnimationState(TrackEntry trackentry, Event e);

        private void OnDestroy()
        {
            _skeletonAnimation.AnimationState.Event -= HandleAnimationState;
        }

        protected virtual void Move() =>
            transform.position += MoveDirection * _speed * Time.deltaTime;
        protected abstract void Attack();
        public abstract void Hit();
    }
}