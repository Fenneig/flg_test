using UnityEngine;

namespace TestGame.Scripts.Components
{
    public class LightningComponent : MonoBehaviour
    {
        [SerializeField] private float _baseLightningSizeInUnits = 4;

        public void ResizeLightning(Vector3 targetPosition)
        {
            var newScale = Mathf.Abs(transform.position.x - targetPosition.x) / _baseLightningSizeInUnits;
            transform.localScale = new Vector3(newScale, 1, 1);
        }

        public void DestroyLightning()
        {
            Destroy(gameObject);
        }
    }
}