using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour {
    
    public float forceAmount = 1.0F;
    
    private Rigidbody _rigidbodyPlayer;
    
    public Transform cameraTransform;
    
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

        // Calcolo la direzione di movimento rispetto all'orientamento della telecamera
        
        // - cameraTransform.forward è un vettore normalizzato della direzione in avanti della camera nello spazio globale
        // - Scale() effettua la moltiplicazione componente per componente
        // - normalized ottiene un vettore normalizzato di lunghezza 1
        
        // in sintesi questa operazione permette di ottenere la direzione in cui la telecamera è rivolta annullandone la
        // componente verticale
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        
        // somma i due vettori per ottenere la direzione di movimento normalizzata lungo l'asse orizzontale o verticale 
        Vector3 moveDirection = (cameraForward * vertical + cameraTransform.right * horizontal).normalized;

        // Applica la forza di movimento al rigidbody del personaggio
        _rigidbodyPlayer.AddForce(moveDirection * forceAmount);
    }
}
