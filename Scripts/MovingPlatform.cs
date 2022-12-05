using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _WPA, _WPB;
    private Transform _destination;
    private Transform _realPlatform;
    private float _speed = 10f;
    private bool _detect;

    // Start is called before the first frame update
    void Start()
    {
        _realPlatform = transform.parent; //Tomando la plataforma física
        _destination = _WPB; //El lugar a donde será desplazada
        _detect = true; //Puede detectar al jugador

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) // other es el jugador
    {
        if (other.tag == "PadrePlayer")
        { 
            Debug.Log("Entró" + other);
            // Revisar si es tag del player
            other.transform.parent = transform; // Hacer que el padre del jugador sea el detector 'trigger'
            if (_detect)
            {
                _detect = false;
                StartCoroutine("Movimiento");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Salió" + other);
        other.transform.parent = null;// Excluímos como hijo de la plataforma a cualquier objeto que se separe de ella
    }

    IEnumerator Movimiento()
    {
        Debug.Log("Moviendo");
        while (_realPlatform.position != _destination.position)
        {
            _realPlatform.position = Vector3.MoveTowards(_realPlatform.position, _destination.position, Time.deltaTime * _speed);
            yield return null;
        }
        if (_destination == _WPA)
        {
            _destination = _WPB;
        }
        else if (_destination == _WPB)
        {
            _destination = _WPA;
        }
        _detect = true;
    }
}
