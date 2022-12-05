using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformPlayer : MonoBehaviour
{
    CharacterController characterController;
    Rigidbody rigidbody;

    Vector3 groundPos;
    Vector3 lastGroundPos;
    Vector3 currentPos;

    string groundName;
    string lastGroundName;

    bool isJump;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Platform")
        {
            if (!isJump)
            {
                RaycastHit hit; //Estructura utilizada para recuperar informacion de una emosión de rayos
                if(Physics.SphereCast(transform.position, rigidbody.angularDrag, -transform.up, out hit))
                {
                    GameObject inGround = hit.collider.gameObject; //Almacenamos el objeto con el que esta colisionando el raycast
                    groundName = inGround.name;
                    groundPos = inGround.transform.position; //Posición del objeto sobre el que estamos

                    if(groundPos != lastGroundPos && groundName == lastGroundName)
                    {
                        currentPos = Vector3.zero; //Reseteamos la posición que avanzara nuestro personaje
                        currentPos += groundPos - lastGroundPos;
                        characterController.Move(currentPos);
                        //rigidbody.Move(currentPos,);
                    }
                    lastGroundName = groundName;
                    lastGroundPos = groundPos;
                }

                /*if (Input.GetKey(KeyCode.Space)) //si el personaje salta se cumplen las sentencias
                {
                    if (!rigidbody.isGrounded) //Si el jugador no esta en el suelo
                    {
                        currentPos = Vector3.zero;
                        lastGroundPos = Vector3.zero;
                        lastGroundName = null;
                        isJump = true;
                    }
                }*/
                if (characterController.isGrounded)
                {
                    isJump = true;
                }

            }
        }
    }

    // Update is called once per frame
}
