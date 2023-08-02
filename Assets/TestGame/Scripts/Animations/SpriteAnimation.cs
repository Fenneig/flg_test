using System;
using TestGame.Scripts.Interfaces;
using TestGame.Scripts.Model;
using UnityEngine;
using UnityEngine.Events;

namespace TestGame.Scripts.Animations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour, IPausable
    {
        [SerializeField] private int _frameRate;
        [SerializeField] private AnimationClip[] _clips;

        private float _secondsPerFrame;
        private float _nextFrameTime;
        private SpriteRenderer _renderer;
        private int _currentFrame;
        private int _currentClip;
        private bool _isPaused; 
        
        private void OnEnable()
        {
            _nextFrameTime = Time.time;
        }

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secondsPerFrame = 1f / _frameRate;
            StartAnimation();
            GameSession.Instance.PauseHandler.Register(this);
        }

        private void StartAnimation()
        {
            _nextFrameTime = Time.time;
            _currentFrame = 0;
        }

        private void Update()
        {
            if (_isPaused || _nextFrameTime > Time.time) return;

            var clip = _clips[_currentClip];

            if (_currentFrame >= clip.Sprites.Length)
            {
                if (clip.Loop)
                {
                    _currentFrame = 0;
                }
                else
                {
                    clip.OnComplete?.Invoke();

                    if (!clip.AllowNextClip) return;
                    
                    _currentFrame = 0;
                    _currentClip = (int) Mathf.Repeat(_currentClip + 1, _clips.Length);
                }

                return;
            }

            _renderer.sprite = clip.Sprites[_currentFrame];
            _nextFrameTime += _secondsPerFrame;
            _currentFrame++;
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private void OnDestroy()
        {
            GameSession.Instance.PauseHandler.Unregister(this);
        }
    }

    [Serializable]
    public class AnimationClip
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _allowNext;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplete;

        public bool IsEqualName(string name) => _name == name;

        public bool Loop => _loop;

        public bool AllowNextClip => _allowNext;

        public Sprite[] Sprites => _sprites;

        public UnityEvent OnComplete => _onComplete;
    }
}