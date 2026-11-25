using UnityEngine;

public class GameClearManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable CameraLook
        CameraLook[] cameras =
            Object.FindObjectsByType<CameraLook>(
                FindObjectsInactive.Include
            );

        foreach (CameraLook cameraLook in cameras)
        {
            cameraLook.enabled = false;
        }

        // Disable PlayerMovement
        PlayerMovement[] players =
            Object.FindObjectsByType<PlayerMovement>(
                FindObjectsInactive.Include
            );

        foreach (PlayerMovement player in players)
        {
            player.enabled = false;
        }

        // Disable PlayerInteraction
        PlayerInteraction[] interactions =
            Object.FindObjectsByType<PlayerInteraction>(
                FindObjectsInactive.Include
            );

        foreach (PlayerInteraction interaction in interactions)
        {
            interaction.enabled = false;
        }
    }
}