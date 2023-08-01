using TestGame.Scripts.Interfaces;
using UnityEngine;

namespace TestGame.Scripts.Environment
{
    public class Exit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<IExitable>(out var exitComponent))
                exitComponent.Exit();
        }
    }
}