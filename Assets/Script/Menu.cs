using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject ScreenHelp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenHelp()
    {
        ScreenHelp.SetActive(true);
    }

    public void CloseHelp()
    {
        ScreenHelp.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
