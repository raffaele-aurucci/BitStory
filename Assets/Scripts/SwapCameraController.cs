using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject _cameraScene;
    [SerializeField]
    private GameObject _cameraPlayer;
    
    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private GameObject _cameraController;

    [SerializeField]
    private GameObject _player;
        
    public void OnClickStartGame()
    {
        // disattivo il panel dove è presente il button
        _panel.SetActive(false);

        // invoco ripetutamente la funzione SwapCamera() 
        InvokeRepeating("SwapCamera", 0.2F, 0.005F*Time.deltaTime);

    }

    // SwapCamera() si occupa di avvicinare la Main Camera alla Camera che segue il giocatore modificando costantemente
    // la posizione e la rotazione della Main Camera rispetto alla poszione della Camera del giocatore
    public void SwapCamera()
    {
        _cameraScene.transform.position = Vector3.Lerp(_cameraScene.transform.position, _cameraPlayer.transform.position,
            0.7F * Time.deltaTime);

        _cameraScene.transform.rotation = Quaternion.Slerp(_cameraScene.transform.rotation,
            _cameraPlayer.transform.rotation, 0.7F * Time.deltaTime);
    }

    public void Update()
    {
        // verifico ad ogni frame se la distanza tra la Main Camera e la camera del giocatore ha raggiunto una certa
        // soglia, in tal caso:
        // - non invoco più SwapCamera()
        // - attivo il controller della camera del giocatore
        // - attivo lo script che gestisce il movimento del giocatore
        // - disabilito la Main Camera
        if (Vector3.Distance(_cameraScene.transform.position, _cameraPlayer.transform.position) < 0.01F)
        {
            CancelInvoke("SwapCamera");
            _cameraController.SetActive(true);
            _player.GetComponent<PlayerController>().enabled = true;
            _cameraScene.SetActive(false);
        }
        
        // Debug.Log("cameraScene: " + cameraScene.transform.position);
        // Debug.Log("cameraPlayer: " + cameraPlayer.transform.position);
    }
}
