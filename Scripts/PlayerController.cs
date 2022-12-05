using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    #region Variables movimiento
    [SerializeField]
    private float _maxSpeed = 10f;
    private float _speed;
    private bool _isRunning;
    [SerializeField]
    private float _horizontalInput, _forwardInput;
    #endregion

    #region Variables de brinco
    private bool _jumpRequest;
    [SerializeField]
    private float _jumpForce = 20f;
    private int _maxJumps=2, _availableJumps = 0;
    #endregion

    #region Animacion
    private PlayerAnimation _playerAnim;
    #endregion

    #region Puntos
    [SerializeField]
    private GameObject _PaneldePuntos;
    private TextMeshProUGUI _puntosTxt;
    private int _puntaje = 0;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        #region Obtener Rigidbody
        _playerRb = GetComponent<Rigidbody>();
        if(_playerRb == null)
        {
            Debug.LogWarning("Jugador no tiene Rigidbody");
        }
        #endregion
        #region Obtener PlayerAnimation
        _playerAnim = GetComponent<PlayerAnimation>();
        if(_playerAnim == null)
        {
            Debug.LogWarning("El jugador no tiene un script PlayerAnimation");
        }
        #endregion
        _speed = _maxSpeed;
        _isRunning = true;
        _puntosTxt = _PaneldePuntos.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Correr/caminar
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _isRunning = !_isRunning;
            if (_isRunning)
            {
                _speed = _maxSpeed / 2;
            }
            else
            {
                _speed = _maxSpeed;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_isRunning)
            {
                _speed = _maxSpeed / 2;
            }
            else
            {
                _speed = _maxSpeed;
            }
        }
        #endregion
        #region Movimiento
        _horizontalInput = Input.GetAxis("Horizontal"); //izq,der
        _forwardInput = Input.GetAxis("Vertical"); //arr, abj

        float velocidad = Mathf.Max(Mathf.Abs(_horizontalInput), Mathf.Abs(_forwardInput));
        velocidad *= _speed / _maxSpeed;
        _playerAnim.SetSpeed(velocidad);

        Vector3 movimiento = new Vector3(_horizontalInput,0,_forwardInput);
        transform.Translate(movimiento*_speed*Time.deltaTime);
        #endregion
        if (Input.GetKeyDown(KeyCode.Space) && _availableJumps>0)
        {
            _jumpRequest = true;
        }
    }

    private void FixedUpdate()
    {
        if (_jumpRequest)
        {
            _playerRb.velocity = Vector3.up*_jumpForce;
            _availableJumps--;
            _jumpRequest=false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _availableJumps = _maxJumps;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("Se encontro un objeto con tag Interactable");
            Interactable interacted= other.GetComponent<Interactable>();
            if(interacted == null)
            {
                Debug.Log("El objeto a interacutar no tiene el script Interactable");
            }
            else
            {
                interacted.Interact(this);
            }
        }
    }
    
    public void puntos(int puntos)
    {
        _puntaje += puntos;
        _puntosTxt.text = $"X {_puntaje.ToString()}";
        //Debug.Log($"El jugador tiene {_puntaje} puntos");
    }

    

}
