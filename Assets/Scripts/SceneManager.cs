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

    // Update is called once per frame
    void Update()
    {
        
    }
}
