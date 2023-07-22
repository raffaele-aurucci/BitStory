using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveCamera : MonoBehaviour
{

    public Transform playerPos;
    
    public float rotationSpeed = 3f;
    
    // distanza camera dall'oggetto player
    public float distance = 5f;
    
    // angolo di rotazione intorno all'asse X inizializzato a 180 gradi
    private float currentX = 180f;
    
    // angolo di rotazione intorno all'asse Y
    private float currentY = 0f;

    private void Start() 
    {
        // Blocca e nasconde il cursore del mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Restituisce l'intensità dell'input del mouse sull'asse X.
        // Questo valore può essere positivo (da 0 a +1) se il mouse viene spostato verso dx
        // o negativo (da 0 a -1) se spostato verso sx.
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;

        // Restituisce l'intensità dell'input del mouse sull'asse Y.
        // Assume valore negativo verso l'alto (da 0 a -1) e positivo verso il basso (da 0 a 1)
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Limita l'angolo di rotazione verticale (currentY) tra 0 gradi e 45 gradi utilizzando
        // la funzione Mathf.Clamp.
        // Se 0 < y < 45 lo restituisce, altrimenti se y < 0 restituisce 0, se y > 45 restituisce 45
        currentY = Mathf.Clamp(currentY, 0, 45f);
    }
    
    // chiamato dopo Update()
    private void LateUpdate()
    {
        // Crea un oggetto Quaternion a partire dagli angoli di rotazione attuali (currentY e currentX),
        // per rappresentare la rotazione desiderata della telecamera.
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);
        
        // calcolo l'offset nel seguente modo:
        // - (rotation * Vector3.forward) produce un vettore che punta nella direzione della vista desiderata dalla camera
        // In Unity, quando si moltiplica un quaternion per un vettore, il quaternion viene utilizzato per ruotare il vettore.
        // Quindi il vettore Vector3.forward è considerato ruotato di x gradi lungo l'asse X e y gradi lungo l'asse Y
        // - distance specifica la distanza tra il player e la camera
        
        Vector3 offset = (rotation * Vector3.forward) * distance;
        Vector3 desiredPosition = playerPos.position - offset;

        // assegna la posizione alla telecamera
        transform.position = desiredPosition;
        
        // punta all'oggetto target, ruota nella direzione dell'oggetto, senza di esso la camera mantiene il suo
        // orientamento inziale
        transform.rotation = rotation;
    }
}
