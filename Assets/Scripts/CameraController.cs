using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private RaycastHit _raycastHit;
    
    // Cambiando questo valore è possibile specificare una rotazione iniziale del personaggio lungo l'asse Y
    [SerializeField]
    private float _mouseX;
    [SerializeField]
    private float _mouseY;

    [SerializeField]
    private float _sensitivity;
    [SerializeField]
    private int _minY = 10;
    [SerializeField]
    private int _maxY = 45;
    
    [SerializeField]
    private Transform _playerPos;
    [SerializeField]
    private Transform _pivotPos;
   
    [SerializeField]
    private Transform _cameraPos;
    [SerializeField]
    private Transform _cameraNormalPos;
    

    // Update is called once per frame
    void Update()
    {
        // Restituisce l'intensità dell'input del mouse sull'asse X.
        // Questo valore può essere positivo (da 0 a +1) se il mouse viene spostato verso dx
        // o negativo (da 0 a -1) se spostato verso sx.
        _mouseX += Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        
        // Restituisce l'intensità dell'input del mouse sull'asse Y.
        // Assume valore negativo verso l'alto (da 0 a -1) e positivo verso il basso (da 0 a 1)
        _mouseY -= Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        _mouseY = Mathf.Clamp(_mouseY, _minY, _maxY);

        // ruoto il pivot, questa rotazione influenzerà anche la rotazione del personaggio
        _pivotPos.transform.rotation = Quaternion.Euler(_mouseY, _mouseX, 0F);
    }

    private void LateUpdate()
    {
        // aggiorno la posizione del CameraController, questo farà in modo che la camera segua il personaggio, mantenendo
        // quindi una distanza costante
        transform.position = _playerPos.position;
        
        // verifica la presenza di collisioni con la camera
        CameraCollision();
    }

    void CameraCollision()
    {
        Debug.DrawLine(_pivotPos.transform.position, _cameraPos.transform.position, Color.red);
        
        // Linecast verifica se c'è una collisione lungo la linea che interccore tra il pivot e la camera
        // la collissione si verifica quando c'è un oggetto a cui è associato un collider lungo la linea
        if (Physics.Linecast(_pivotPos.transform.position, _cameraPos.transform.position, out _raycastHit))
        {
            // avvicina la camera al personaggio
            _cameraPos.transform.position = Vector3.Lerp(_cameraPos.transform.position, _raycastHit.point, 10F * Time.deltaTime);
        }
        else
        {
            // allontana la camera dal personaggio
            _cameraPos.transform.position = Vector3.Lerp(_cameraPos.transform.position, _cameraNormalPos.transform.position,
                0.7F * Time.deltaTime);
        }
    }
}
