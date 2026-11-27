using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerOwnershipManager : MonoBehaviour
{
    public NetworkObject[] players = new NetworkObject[4];

    private NetworkManager networkManager;

    private Dictionary<ulong, int> assignedPlayers = new Dictionary<ulong, int>();

    void Start()
    {
        networkManager = NetworkManager.Singleton;

        if (networkManager == null)
        {
            Debug.LogError("NetworkManager not found!");
            return;
        }

        // Only the Host/Server assigns players.
        if (!networkManager.IsServer)
            return;

        networkManager.OnClientDisconnectCallback += OnClientDisconnected;
    }

    void Update()
    {
        if (networkManager == null || !networkManager.IsListening || !networkManager.IsServer)
            return;

        if (!AllPlayersSpawned())
            return;

        AssignPlayer(NetworkManager.ServerClientId);
        foreach (ulong clientId in networkManager.ConnectedClientsIds)
        {
            if (clientId != NetworkManager.ServerClientId)
            {
                AssignPlayer(clientId);
            }
        }
    }

    bool AllPlayersSpawned()
    {
        if (players == null || players.Length != 4)
            return false;

        foreach (NetworkObject player in players)
        {
            if (player == null || !player.IsSpawned)
                return false;
        }

        return true;
    }

    void AssignPlayer(ulong clientId)
    {
        if (assignedPlayers.ContainsKey(clientId))
            return;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null)
                continue;

            if (!assignedPlayers.ContainsValue(i))
            {
                players[i].ChangeOwnership(clientId);

                Debug.Log("Actual Owner ID: " + players[i].OwnerClientId);

                assignedPlayers.Add(clientId, i);

                Debug.Log("Client " + clientId + " assigned to Player " + (i + 1));
                return;
            }
        }

        Debug.LogWarning("No available player for Client " + clientId);
    }

    void OnClientDisconnected(ulong clientId)
    {
        if (!assignedPlayers.ContainsKey(clientId))
            return;

        int playerIndex = assignedPlayers[clientId];

        assignedPlayers.Remove(clientId);

        if (players[playerIndex] != null && players[playerIndex].IsSpawned)
        {
            players[playerIndex].ChangeOwnership(NetworkManager.ServerClientId);
        }

        Debug.Log("Player " + (playerIndex + 1) + " returned to the Host.");
    }

    void OnDestroy()
    {
        if (networkManager != null)
        {
            networkManager.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }
}

