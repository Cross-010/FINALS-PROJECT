using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    public int requiredQuota = 5;
    public TMP_Text quotaText;

    void Start()
    {
        UpdateQuotaUI();
    }

    public void AddScore()
    {
        if (NetworkManager.Singleton == null ||
        !NetworkManager.Singleton.IsServer)
            return;

        score++;

        UpdateQuotaUI();

        Debug.Log("Current Quota: " + score + "/" + requiredQuota);

        if (QuotaReached())
        {
            Debug.Log("QUOTA REACHED! GAME CLEAR!");

            NetworkManager.Singleton.SceneManager.LoadScene(
                "Game Clear",
                LoadSceneMode.Single
            );
        }
    }

    public void DecreaseScore()
    {
        score = Mathf.Max(0, score - 1);
        UpdateQuotaUI();
    }

    void UpdateQuotaUI()
    {
        if (quotaText != null)
        {
            quotaText.text = score + " / " + requiredQuota;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public bool QuotaReached()
    {
        return score >= requiredQuota;
    }
}