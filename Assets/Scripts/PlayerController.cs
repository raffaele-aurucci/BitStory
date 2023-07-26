using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private CharacterController _controller;
    [SerializeField] 
    private Animator _animator;
    [SerializeField]
    private float _playerSpeed = 0.4F;
    [SerializeField]    
    private float _gravity = -9.81F;
    
    private Vector3 _playerVelocity;
    private float _horizontal;
    private float _vertical;
    
    [SerializeField]
    private Transform _pivot;

    // Update is called once per frame
    void Update()
    {
        // Restituisce l'intensità dell'input sull'asse orizzontale (-1 a 1) (A e D, freccia su e freccia giu)
        _horizontal = Input.GetAxis("Horizontal");
        
        // Restituisce l'intensità dell'input sull'asse verticale (-1 a 1) (W e S - freccia su e giu)
        _vertical = Input.GetAxis("Vertical");
        
        // Setto i valori float a vertical e a horizontal per applicare l'animazione, uso dampTime per avere un
        // cambio di animazione meno brusco e più fluido
        _animator.SetFloat("horizontal", _horizontal, 0.07F, Time.deltaTime);
        _animator.SetFloat("vertical", _vertical, 0.07F, Time.deltaTime);

        Vector3 movement = transform.forward * _vertical + transform.right * _horizontal;
        _controller.Move(movement * _playerSpeed * Time.deltaTime);
        
        // Controllo se c'è contatto con il terreno per applicare la gravità
        bool isGround = Physics.Raycast(transform.position, Vector3.down, 0.35F);

        if (isGround && _playerVelocity.y < 0)
            _playerVelocity.y = -2F;

        // Applica la forza di gravità al personaggio
        _playerVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
    
    void LateUpdate()
    {
        // Applica la rotazione all'asse Y del personaggio quando il pivot viene ruotato
        transform.rotation = Quaternion.Euler(0F, _pivot.eulerAngles.y, 0F);
    }
}
