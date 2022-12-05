using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); //GameManager
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #region Componentes del panel de dialogos
    [Header("Panel de dialogos")]
    #pragma warning disable 0649 
    [SerializeField]
    private GameObject _dialoguePanel;
    [SerializeField]
    #pragma warning restore 0649
    private Button _nextBttn;
    private TextMeshProUGUI _dialogueTxt, _nameTxt, _nextTxt;
    #endregion

    #region Variables NPC
    private string _name;
    private List<string> _dialogueList;
    private int _dialogueIdx = 0;
    #endregion

    #region
    [SerializeField]
    private AudioSource _cancion;
    [SerializeField]
    private GameObject _ganaste;
    [SerializeField]
    private AudioSource _cancionInicio;
    #endregion
    private TextMeshProUGUI _ganasteTexto;
    // Start is called before the first frame update
    void Start()
    {
        #region Iniciar componentes del panel de dialogos
        if (_dialoguePanel == null)
        {
            Debug.LogError("No se agrego el panel de dialogos");
        }
        else
        {
            _dialogueTxt=_dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
            if(_dialogueTxt == null)
            {
                Debug.LogWarning("No se encontró un cuadro de dialogos en el panel");
            }
            /*else
            {
                _dialogueTxt.text = "Encontrado texto de dialogos";
            }*/

            _nameTxt = _dialoguePanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
            if (_dialogueTxt == null)
            {
                Debug.LogWarning("No se encontró un texto para nombre en el panel");
            }/*
            else
            {
                _nameTxt.text = "Encontrado texto para nombre de NPC";
            }*/

            _nextBttn = _dialoguePanel.transform.GetChild(2).GetComponent<Button>();
            if (_dialogueTxt == null)
            {
                Debug.LogWarning("No se encontró el botón NEXT");
            }
            else
            {
                //Agregar Listener
                _nextBttn.onClick.AddListener(delegate { ContinueDialogue(); });
                _nextTxt = _nextBttn.GetComponentInChildren<TextMeshProUGUI>();
                if (_nextBttn == null)
                {
                    Debug.LogWarning("No se encontro un botón de continuar");
                }
                else
                {
                    _nextTxt.text = "SIGUIENTE";
                }
            }
        }
        #endregion
        _dialoguePanel.SetActive(false);
        //_cancion.GetComponent<AudioSource>();
        _ganasteTexto = _ganaste.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void setDialogue(string name, string[] dialogue)
    {
        _name = name;
        _dialogueList = new List<string>(dialogue.Length);
        _dialogueList.AddRange(dialogue);
        //_dialogueIdx = 0;
        _nameTxt.text = _name;
        _nextTxt.text = "Continuar";
        _dialoguePanel.SetActive(true);
    }

    private void ShowDialogue()
    {
        //Debug.Log($"Dialogo # {_dialogueIdx}");
        _dialogueTxt.text = _dialogueList[_dialogueIdx]; //Función de mostrar
    }

    private void ContinueDialogue()
    {
        if (_dialogueIdx == _dialogueList.Count - 1)//Se terminan los diálogos
        {

            //Debug.Log("Se termina el diálogo con " + _name);
            _dialoguePanel.SetActive(false);
            _ganaste.SetActive(true);
            _cancionInicio.Stop();
            _cancion.Play();
            Time.timeScale = 0;
            
        }
        else if (_dialogueIdx == _dialogueList.Count - 2)//Penúltimo
        {
            _nextTxt.text = "Salir";
            _dialogueIdx++;
            ShowDialogue();
        }
        else
        {
            _dialogueIdx++;
            ShowDialogue();
        }

    }
}
