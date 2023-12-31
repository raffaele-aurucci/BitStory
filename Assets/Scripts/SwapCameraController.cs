using UnityEngine;

public class SwapCameraController : MonoBehaviour
{
    #region fields
    
    [SerializeField]
    private GameObject cameraScene;
    
    [SerializeField]
    private GameObject cameraPlayer;
    
    [SerializeField]
    private GameObject startPanel;

    [SerializeField]
    private GameObject player;
    
    #endregion
    
    public void OnClickStartGame()
    {
        AudioManager.current.PlayStartGameSound();
        
        // disattivo il panel dove è presente il button
        startPanel.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // invoco ripetutamente la funzione SwapCamera() 
        InvokeRepeating("SwapCamera", 0.2F, 0.005F*Time.deltaTime);

    }

    // SwapCamera() si occupa di avvicinare la Main Camera alla Camera che segue il giocatore modificando costantemente
    // la posizione e la rotazione della Main Camera rispetto alla poszione della Camera del giocatore
    public void SwapCamera()
    {
        cameraScene.transform.position = Vector3.Lerp(cameraScene.transform.position, cameraPlayer.transform.position,
            0.7F * Time.deltaTime);

        cameraScene.transform.rotation = Quaternion.Slerp(cameraScene.transform.rotation,
            cameraPlayer.transform.rotation, 0.7F * Time.deltaTime);
    }

    public void Update()
    {
        // verifico ad ogni frame se la distanza tra la Main Camera e la camera del giocatore ha raggiunto una certa
        // soglia, in tal caso:
        // - non invoco più SwapCamera()
        if (Vector3.Distance(cameraScene.transform.position, cameraPlayer.transform.position) < 0.01F)
        {
            CancelInvoke("SwapCamera");
            ChangeSceneController.current.LoadScene();
        } //necessario per non far avvertire lo stacco di animazione durante il cambio scena
        else if (Vector3.Distance(cameraScene.transform.position, cameraPlayer.transform.position) < 10F)
        {
            player.GetComponentInChildren<Animator>().enabled = true;
        }
        
    }
}
