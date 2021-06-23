using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject buttonsPanel;

    // Start is called before the first frame update
    void Start()
    {
        warningPanel.SetActive(true);
        buttonsPanel.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseWarningPanel()
    {
        warningPanel.SetActive(false);
        buttonsPanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Debug.Log("Stopping Game");
        Application.Quit();
    }
}
