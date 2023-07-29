using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    private RaycastHit raycastHit;
    
    // Cambiando questo valore è possibile specificare una rotazione iniziale del personaggio lungo l'asse Y
    [SerializeField]
    private float mouseX;
    [SerializeField]
    private float mouseY;

    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private int minDown = 0;
    [SerializeField]
    private int maxUp = 45;
    
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private Transform pivotPos;
   
    [SerializeField]
    private Transform cameraPos;
    [SerializeField]
    private Transform cameraNormalPos;
    

    // Update is called once per frame
    void Update()
    {
        // Restituisce l'intensità dell'input del mouse sull'asse X.
        // Questo valore può essere positivo (da 0 a +1) se il mouse viene spostato verso dx
        // o negativo (da 0 a -1) se spostato verso sx.
        mouseX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        
        // Restituisce l'intensità dell'input del mouse sull'asse Y.
        // Assume valore negativo verso l'alto (da 0 a -1) e positivo verso il basso (da 0 a 1)
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, minDown, maxUp);

        // ruoto il pivot, questa rotazione influenzerà anche la rotazione del personaggio
        pivotPos.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0F);
    }

    private void LateUpdate()
    {
        // aggiorno la posizione del CameraController, questo farà in modo che la camera segua il personaggio, mantenendo
        // quindi una distanza costante
        transform.position = playerPos.position;
        
        // verifica la presenza di collisioni con la camera
        CameraCollision();
    }

    void CameraCollision()
    {
        Debug.DrawLine(pivotPos.transform.position, cameraPos.transform.position, Color.red);
        
        // Linecast verifica se c'è una collisione lungo la linea che interccore tra il pivot e la camera
        // la collissione si verifica quando c'è un oggetto a cui è associato un collider lungo la linea
        if (Physics.Linecast(pivotPos.transform.position, cameraPos.transform.position, out raycastHit))
        {
            // avvicina la camera al personaggio
            cameraPos.transform.position = Vector3.Lerp(cameraPos.transform.position, raycastHit.point, 10F * Time.deltaTime);
        }
        else
        {
            // allontana la camera dal personaggio
            cameraPos.transform.position = Vector3.Lerp(cameraPos.transform.position, cameraNormalPos.transform.position,
                0.7F * Time.deltaTime);
        }
    }
}
