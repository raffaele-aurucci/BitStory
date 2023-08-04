using UnityEngine;

namespace AdaGame
{
    public class TriggerPlatformController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameObject gameObject = other.gameObject;

            if (gameObject.CompareTag("Sphere"))
            {
                GameObject parent = transform.parent.gameObject;
                Invoke("IncrementSteps", 0.8F);
                Destroy(parent, 0.8F);
            
            }
        }

        private void IncrementSteps()
        {
            UIManagerController.current.steps += 1;
        }
    }
}
