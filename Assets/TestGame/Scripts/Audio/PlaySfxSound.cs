using UnityEngine;

namespace TestGame.Scripts.Audio
{
    public class PlaySfxSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private AudioSource _source;

        public void Play() 
        {
            _source.PlayOneShot(_clip);
        }
    }
}