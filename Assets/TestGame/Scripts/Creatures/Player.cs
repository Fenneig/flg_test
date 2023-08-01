using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

namespace TestGame.Scripts.Creatures
{
    public class Player : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private string _runAnimation;
        [SerializeField] private string _shootAnimation;
        
        [Header("Gun")] 
        [SerializeField] private Transform _barrelTransform;
        [SerializeField] private GameObject _bulletPrefab;
        
        [Header("Player settings")] 
        [SerializeField, Range(0, 10)] private float _speed;

        private bool _isFiring;
        
        private void Start()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, _runAnimation, true);
            _skeletonAnimation.AnimationState.Event += HandleAnimationState;
        }

        private void Update()
        {
            if (!_isFiring) transform.position += Vector3.right * _speed * Time.deltaTime;
            
            if (Input.GetMouseButtonDown(0))
            {
                _isFiring = true;
                _skeletonAnimation.AnimationState.SetAnimation(0, _shootAnimation, false);
                _skeletonAnimation.AnimationState.AddAnimation(0, _runAnimation, true, 0);
            }
        }

        private void HandleAnimationState(TrackEntry trackentry, Event e)
        {
            if (e.Data.Name.Contains("fire")) _isFiring = false;
        }

        private void OnDestroy()
        {
            _skeletonAnimation.AnimationState.Event -= HandleAnimationState;
        }
    }
}
