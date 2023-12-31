using Spine;
using TestGame.Scripts.Audio;
using TestGame.Scripts.Components;
using TestGame.Scripts.Interfaces;
using TestGame.Scripts.Model;
using UnityEngine;
using Event = Spine.Event;

namespace TestGame.Scripts.Creatures
{
    public class Player : Character, IExitable
    {
        [Header("Gun"), Space] 
        [SerializeField] private Transform _barrelTransform;
        [SerializeField] private LightningComponent _lightningPrefab;
        [SerializeField] private PlaySfxSound _sfxSound;

        private RaycastHit2D[] _results = new RaycastHit2D[10];
        private GameObject _target;

        protected override void Update()
        {
            if (_skeletonAnimation.AnimationName != _attackAnimation && Input.GetMouseButtonDown(0))
                Attack();
            
            base.Update();
        }

        protected override void HandleAnimationState(TrackEntry trackentry, Event e)
        {
            if (e.Data.Name.ToLower().Contains("fire"))
            {
                var lightning = Instantiate(_lightningPrefab, _barrelTransform.position, Quaternion.identity);
                lightning.ResizeLightning(_target.transform.position);
                _target.GetComponent<IDamageable>().Hit();
            }
        }
    
        protected override void Attack()
        {
            Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.RaycastNonAlloc(origin, Vector2.zero, _results, float.MaxValue, _foeLayerMask) > 0)
            {
                _target = _results[0].transform.gameObject;
                _skeletonAnimation.AnimationState.SetAnimation(0, _attackAnimation, false);
                _skeletonAnimation.AnimationState.AddAnimation(0, _runAnimation, true, 0);
                _sfxSound.Play();
            }
        }

        public override void Hit()
        {
            GameSession.Instance.GameOver("Lose!");
        }

        public void Exit()
        {
            GameSession.Instance.GameOver("Win!");
        }
    }
}