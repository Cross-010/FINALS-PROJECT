using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerConnection : MonoBehaviour
{
    public string gameplayScene = "Main Level";

    public void StartHost()
    {
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager was not found!");
            return;
        }

        bool started = NetworkManager.Singleton.StartHost();

        if (started)
        {
            Debug.Log("Host started successfully!");

            NetworkManager.Singleton.SceneManager.LoadScene(gameplayScene,LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Failed to start Host!");
        }
    }

    public void StartClient()
    {
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager was not found!");
            return;
        }

        bool started = NetworkManager.Singleton.StartClient();

        if (started)
        {
            Debug.Log("Client connection started!");
        }
        else
        {
            Debug.LogError("Failed to start Client!");
        }
    }

    public void Disconnect()
    {
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsListening)
        {
            NetworkManager.Singleton.Shutdown();
            Debug.Log("Disconnected from multiplayer session.");
        }
    }
}