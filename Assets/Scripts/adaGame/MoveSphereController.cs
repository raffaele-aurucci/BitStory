using UnityEngine;

namespace AdaGame
{
    public class MoveSphereController : MonoBehaviour
    {
        [SerializeField]
        [Header("The speed associated at movement of sphere")]
        private float speed;

        private Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = this.gameObject.GetComponent<Rigidbody>();    
        }

        // FixedUpdate mi permette di gestire con più precisione la fisica associata al movimento della sfera
        void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
        
            Vector3 movement = transform.forward * vertical + transform.right * horizontal;
            movement = movement.normalized;

            if (Physics.Raycast(transform.position, Vector3.down, 1.0F))
            {
                // fa arrestare la pallina quando non c'è input
                if (Mathf.Approximately(horizontal, 0f) && Mathf.Approximately(vertical, 0f))
                {
                    rb.velocity = Vector3.zero;
                }
                else
                {
                    rb.AddForce(movement * speed, ForceMode.Impulse);
                }
            
                // mantiene la pallina alla massima velocità indicata
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
            }
            else
            {
                // gestisce il caso limite in cui non si faccia muovere la pallina alla max velocità costante, fa in modo 
                // che raycast possa rilevare una nuova piattaforma se il centro della sfera è ben allineato con un altra
                // piattaforma
                Invoke("AnotherControlForGameOver", 0.5F);
            }
        }

        void AnotherControlForGameOver()
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 1.0F))
            {
                rb.constraints = RigidbodyConstraints.None;
                // fa apparire il gameOver quando la pallina cade nel vuoto
                UIManagerController.current.GameOverPanelAppearNextFallSphere();
            }
        }
    }
}
