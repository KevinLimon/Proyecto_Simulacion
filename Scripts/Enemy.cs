using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private GameObject _objetivo;
    private float _speed=5f, _distObjv, _angleObjv;
    private PlayerAnimation _playerAnim;
    private float _speedAnim = 5f;
    private float _horizontalInput, _forwardInput;
    private bool _canAttack;
    private Animator _enemyAnim;
    private float _lastAttack, _AttackCooldown=2f;
    private Transform _EnemyCollisionDetector;
    private float _colliderRadius = 0.5f;
    [SerializeField]
    private Transform _jugador;
    [SerializeField]
    private Transform _inicio;
    [SerializeField]
    private AudioSource _youLost;
    // Start is called before the first frame update
    void Start()
    {
        _objetivo = GameObject.FindGameObjectWithTag("Player");
        _EnemyCollisionDetector = transform.GetChild(0);
        if(_objetivo == null)
        {
            Debug.Log("No hay objetivo con tag Player");
        }
        

        _playerAnim = GetComponent<PlayerAnimation>();
        _enemyAnim = GetComponent<Animator>();
        if (_playerAnim == null)
        {
            Debug.LogWarning("El Enemigo no tiene un script PlayerAnimation");
        }
        /*else
        {
            Debug.Log("Si hay script PlayerAnimation");
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        if (LookingAtPlayer())
        {
            FollowPlayer();
            //Debug.Log($"Jugador");
        }
        else
        {
           // Debug.Log($"?????");
        }
    }

    private void BlindSearch()
    {
        if (Physics.CheckSphere(_EnemyCollisionDetector.position, _colliderRadius, 1 << 6))
        {
            if(Random.value > 0.5f)
            {
                transform.Rotate(0, 90, 0);
            }
        }
        else
        {
            transform.Rotate(0, -90, 0);
        }
    }

    private void FollowPlayer()
    {
        Vector3 GroundedObjective = new Vector3( _objetivo.transform.position.x, transform.position.y, _objetivo.transform.position.z );
        transform.LookAt(GroundedObjective);
        transform.LookAt(_objetivo.transform.position);
        _distObjv = Vector3.Distance(_objetivo.transform.position, transform.position);

        //velocidad *= _speed;
        if (_distObjv <= 1) //Atacar
        {
            _jugador.position = _inicio.position;
            _playerAnim.SetSpeed(0);
            _youLost.Play();

        }
        else
        {
            //_canAttack = false;
            _playerAnim.SetSpeed(1f);
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }

    private void Attack()
    {
        Debug.Log("Murio");
        _jugador = _inicio;
        _canAttack = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Daño de una vida");
    }

    private bool LookingAtPlayer()
    {
        _distObjv = Vector3.Distance(_objetivo.transform.position, transform.position);
        //Debug.Log($"Posicion {_distObjv}");
        _angleObjv = Vector3.Angle(_objetivo.transform.position - transform.position, transform.forward);
        //Debug.Log($"Angulo {_angleObjv}");

        if (_angleObjv <= 120 && _distObjv <= 15 || _distObjv <= 3) //Detecta
        {
            return true;
        }
        else //No lo detecta
        {
            return false;
        }
    }
}
