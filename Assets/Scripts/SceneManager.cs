using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene("Main Level");
    }

    void Update()
    {
        
    }
}
