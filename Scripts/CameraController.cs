using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables inicializacion camara
    [SerializeField]
    private Transform _jugador, _camara, _pivote;
    private float _cameraHeight = 5;
    #endregion
    #region Variables zoom
    [SerializeField]
    private float _zoom = -20;
    [SerializeField]
    private float _zoomSpeed = 3;
    [SerializeField]
    private float _zoomMax = -3f, _zoomMin=-20f;
    #endregion
    #region Variables rotación
    [SerializeField]
    private float _camRotX, _camRotY;
    [SerializeField]
    private float _mouseSensitivity = 2;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Comprobar asignacion de elementos
        if (_jugador == null)
        {
            Debug.Log("Falta asignar el personaje al CameraController.cs");
        }
        if (_camara == null)
        {
            Debug.Log("Falta asignar la camara al CameraController.cs");
        }
        if (_pivote == null)
        {
            Debug.Log("Falta asignar el pivote/punto de foco al CameraController.cs");
        }
        #endregion

        #region asignar parentesco
        _pivote.SetParent(_jugador);
        _camara.SetParent(_pivote);
        #endregion

        #region Asignar posición y rotación
        _pivote.localPosition = new Vector3(0, _cameraHeight, 0);
        _pivote.localRotation = Quaternion.Euler(0, 0, 0);
        _pivote.localScale = new Vector3(1, 1, 1);
        _camara.localPosition = new Vector3(0,-1,_zoom);
        _camara.LookAt(_jugador);
        _camara.localScale = new Vector3(1, 1, 1);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region zoom
        _zoom += Input.GetAxis("Mouse ScrollWheel")*_zoomSpeed;
        _zoom = Mathf.Clamp(_zoom, _zoomMin, _zoomMax);
        _camara.localPosition = new Vector3(0, -1, _zoom);
        #endregion
        #region Rotacion
        if (Input.GetMouseButton(1))
        {
            _camRotX += Input.GetAxis("Mouse X");
            _camRotY += Input.GetAxis("Mouse Y");
            _camRotY = Mathf.Clamp(_camRotY, -20, 70);
            _pivote.localRotation = Quaternion.Euler(_camRotY, 0, 0);
            _jugador.localRotation = Quaternion.Euler(0, _camRotX, 0);
        }
        

        #endregion
        _camara.LookAt(_jugador);
    }
}
