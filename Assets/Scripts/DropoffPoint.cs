using UnityEngine;
using System.Collections.Generic;

public class DropoffPoint : MonoBehaviour
{
    public ScoreManager scoreManager;

    private HashSet<GameObject> processedItems = new HashSet<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        GameObject item;

        if (other.attachedRigidbody != null)
        {
            item = other.attachedRigidbody.gameObject;
        }
        else
        {
            item = other.gameObject;
        }

        if (!item.CompareTag("Item"))
            return;

        if (processedItems.Contains(item))
            return;

        processedItems.Add(item);

        scoreManager.AddScore();

        Destroy(item);
    }
}
