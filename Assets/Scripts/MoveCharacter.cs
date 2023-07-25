using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public float speed;
    public Animator animator;
    private Rigidbody _rigidbodyPlayer;

    void Start() 
    {
        _rigidbodyPlayer = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        // Restituisce l'intensità dell'input sull'asse orizzontale (-1 a 1) (A e D, freccia su e freccia giu)
        float horizontal = Input.GetAxis("Horizontal");
        
        // Restituisce l'intensità dell'input sull'asse verticale (-1 a 1) (W e S - freccia su e giu)
        float vertical = Input.GetAxis("Vertical");

        // calcolo il movimento del giocatore
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;
        movement.Normalize();
        
        transform.position += movement * speed * Time.deltaTime;
        
        // setto i valori float a vertical e a horizontal per applicare l'animazione
        animator.SetFloat("vertical", vertical);
        animator.SetFloat("horizontal", horizontal);
    }
}
