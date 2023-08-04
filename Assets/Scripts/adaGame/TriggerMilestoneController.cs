using UnityEngine;

namespace AdaGame
{
    public class TriggerMilestoneController : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Sphere"))
            {
                Invoke("InvokeFinishGame", 0.5F);
            }
        }
        
        void InvokeFinishGame()
         {
             UIManagerController.current.FinishGame();    
         }
    }

    
}
