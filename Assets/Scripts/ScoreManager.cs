using UnityEngine;
using TMPro;

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
        score++;
        UpdateQuotaUI();
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