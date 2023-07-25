using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveCamera : MonoBehaviour
{
    public Transform playerTransform;
    public float sensitivity = 150F;

    private float _verticalRotation;
    private float _horizontalRotation;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // considera la rotazione verticale rispetto agli assi locali del parent
        _verticalRotation = playerTransform.localEulerAngles.x;
        
        // considera la rotazione orizzontale rispetto agli assi globali
        _horizontalRotation = playerTransform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Restituisce l'intensità dell'input del mouse sull'asse X.
        // Questo valore può essere positivo (da 0 a +1) se il mouse viene spostato verso dx
        // o negativo (da 0 a -1) se spostato verso sx.
        float mouseX = Input.GetAxis("Mouse X");

        // Restituisce l'intensità dell'input del mouse sull'asse Y.
        // Assume valore negativo verso l'alto (da 0 a -1) e positivo verso il basso (da 0 a 1)
        float mouseY = -Input.GetAxis("Mouse Y");

        // calcolo la rotazione verticale
        _verticalRotation += mouseY * sensitivity * Time.deltaTime;
        
        // calcolo la rotazione orizzontale
        _horizontalRotation += mouseX * sensitivity * Time.deltaTime;

        // limito l'angolo di rotazione verticale
        _verticalRotation = Mathf.Clamp(_verticalRotation, -10F, 30F);
        
        // applico la rotazione verticale alla camera lungo l'asse X
        transform.localRotation = Quaternion.Euler(_verticalRotation, 0F, 0F);

        // applico la rotazione orizzontale al giocatore lungo l'asse Y
        playerTransform.rotation = Quaternion.Euler(0F, _horizontalRotation, 0F);
    }

    
}
