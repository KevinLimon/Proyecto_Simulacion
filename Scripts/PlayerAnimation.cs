using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        if(_playerAnimator == null)
        {
            Debug.LogWarning("El primer hijo del jugador no tiene animator");
        }
    }
    public void SetSpeed(float speed)
    {
        _playerAnimator.SetFloat("Speed", speed);
    }

}
