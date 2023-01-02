using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{    
    [SerializeField] GameObject _InformationPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame() {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void InformationPanel() {
        _InformationPanel.SetActive(true);
    }

    public void GoHome() {
        _InformationPanel.SetActive(false);
    }
}
