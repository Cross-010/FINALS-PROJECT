using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DropoffPoint : MonoBehaviour
{
    public ScoreManager scoreManager;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("DROP-OFF TRIGGER DETECTED: " + other.name);

        GameObject item = other.attachedRigidbody != null
            ? other.attachedRigidbody.gameObject
            : other.gameObject;

        Debug.Log("Detected object: " + item.name);
        Debug.Log("Object tag: " + item.tag);

        if (!item.CompareTag("Item"))
        {
            Debug.Log("Object is not tagged Item!");
            return;
        }

        if (NetworkManager.Singleton != null &&
            !NetworkManager.Singleton.IsServer)
        {
            Debug.Log("Not the server. Ignoring drop-off.");
            return;
        }

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager is not assigned!");
            return;
        }

        Debug.Log("Item accepted! Adding score.");

        scoreManager.AddScore();
        Destroy(item);
    }
}