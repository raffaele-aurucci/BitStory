using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region fields
    
    [SerializeField] 
    private CharacterController controller;
    [SerializeField] 
    private Animator animator;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]    
    private float gravity = -9.81F;
    
    private Vector3 playerVelocity;
    private float horizontal;
    private float vertical;
    
    [SerializeField]
    private Transform pivot;
    
    #endregion
    
    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
        
    void Update()
    {
        // Restituisce l'intensità dell'input sull'asse orizzontale (-1 a 1) (A e D, freccia su e freccia giu)
        horizontal = Input.GetAxis("Horizontal");
        
        // Restituisce l'intensità dell'input sull'asse verticale (-1 a 1) (W e S - freccia su e giu)
        vertical = Input.GetAxis("Vertical");
        
        AudioManager.current.PlayStepSound();

        // Setto i valori float a vertical e a horizontal per applicare l'animazione, uso dampTime per avere un
        // cambio di animazione meno brusco e più fluido
        animator.SetFloat("horizontal", horizontal, 0.07F, Time.deltaTime);
        animator.SetFloat("vertical", vertical, 0.07F, Time.deltaTime);

        Vector3 movement = transform.forward * vertical + transform.right * horizontal;
        controller.Move(movement * playerSpeed * Time.deltaTime);
        
        // Controllo se c'è contatto con il terreno per applicare la gravità
        bool isGround = Physics.Raycast(transform.position, Vector3.down, 0.35F);

        if (isGround && playerVelocity.y < 0)
            playerVelocity.y = -2F;

        // Applica la forza di gravità al personaggio
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    
    void LateUpdate()
    {
        // Applica la rotazione all'asse Y del personaggio quando il pivot viene ruotato
        transform.rotation = Quaternion.Euler(0F, pivot.eulerAngles.y, 0F);
    }
}
