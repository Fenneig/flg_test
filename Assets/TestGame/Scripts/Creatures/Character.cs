using Spine;
using Spine.Unity;
using TestGame.Scripts.Interfaces;
using TestGame.Scripts.Model;
using UnityEngine;
using Event = Spine.Event;

namespace TestGame.Scripts.Creatures
{
    public abstract class Character : MonoBehaviour, IDamageable, IPausable
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

        private float _animationTimeScale;
        private bool _isPaused;
        
        protected virtual void Start()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _runAnimation, true);
            _skeletonAnimation.AnimationState.Event += HandleAnimationState;
            _animationTimeScale = _skeletonAnimation.timeScale;
            GameSession.Instance.PauseHandler.Register(this);
        }

        protected virtual void Update()
        {
            if (_skeletonAnimation.AnimationName == _runAnimation) Move();
        }

        protected virtual void HandleAnimationState(TrackEntry trackentry, Event e)
        {
            Debug.Log($"Character: {gameObject.name} do {e.Data.Name} animation event without handling it!");
        }

        private void OnDestroy()
        {
            _skeletonAnimation.AnimationState.Event -= HandleAnimationState;
            GameSession.Instance.PauseHandler.Unregister(this);
        }

        protected virtual void Move()
        {
            var currentSpeed = _isPaused ? 0 : _speed;
            transform.position += MoveDirection * currentSpeed * Time.deltaTime;
        }

        protected abstract void Attack();
        public abstract void Hit();
        
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
            
            if (isPaused)
            {
                _animationTimeScale = _skeletonAnimation.timeScale;
                _skeletonAnimation.timeScale = 0;
            }
            else
            {
                _skeletonAnimation.timeScale = _animationTimeScale;
            }
        }
    }
}