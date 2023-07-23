using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private InputField _nameInputField;
    [SerializeField] private Button _startButton;

    private void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _startButton.interactable = false;
    }

    public void SetButtonInteractable()
    {
        if (_nameInputField.text.Length > 0)
        {
            _startButton.interactable = true;
        }
        else
        {
            _startButton.interactable = false;
        }
    }

    private void StartGame()
    {
        GameManager.playerName = _nameInputField.text;
        SceneManager.LoadScene("Game");
    }
}