using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    int lines;
    public int level = 0;
    public bool DidLevelUp = false;
    public int LinesPerLevel = 5;

    const int MinLines = 1;
    const int MaxLines = 4;

    public Text LinesText;
    public Text CoinsTextMenu;
    public Text CoinsTextGame;
    public Text LevelText;
    public Text ScoreText;
    public Text HighScore;

    public void ScoreLines(int n)
    {
        n = Mathf.Clamp(n, MinLines, MaxLines);
        switch(n)
        {
            case 1:
                score += 40 * level;
                break;
            case 2:
                score += 100 * level;
                break;
            case 3:
                score += 300 * level;
                break;
            case 4:
                score += 1200 * level;
                break;
        }  
        lines -= n;
        if(lines <= 0)
        {
            LevelUp();
        }
        UpdateUIText();
      
    }
    public void Reset()
    {
        level = 1;
        lines = LinesPerLevel * level;
        score = 0;
        UpdateUIText();
    }
    // Start is called before the first frame update
    void Start()
    {
        HighScore.text = AdedZero(PlayerPrefs.GetInt("score", 0), 5);
        CoinsTextMenu.text = AdedZero(PlayerPrefs.GetInt("coins", 0), 5);
        CoinsTextGame.text = AdedZero(PlayerPrefs.GetInt("coins", 0), 5);
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateUIText()
    {
        if (LinesText)
        {
            LinesText.text = lines.ToString();
        }
        if (LevelText)
        {
            LevelText.text = level.ToString();
        }
        if (ScoreText)
        {
            ScoreText.text = AdedZero(score, 5);
        }
        if (CoinsTextGame)
        {
            CoinsTextGame.text = AdedZero(PlayerPrefs.GetInt("coins", 0), 5);
        }
        if (CoinsTextMenu)
        {
            CoinsTextMenu.text = AdedZero(PlayerPrefs.GetInt("coins", 0), 5);
        }
    }
    string AdedZero(int n, int ZeroDigits)
    {
        string nStr = n.ToString();
        while(nStr.Length < ZeroDigits)
        {
            nStr = "0" + nStr;
        }
        return nStr;
    }
    public void LevelUp()
    {
        level++;
        lines = LinesPerLevel * level;
        DidLevelUp = true;
    }
    public void SetCoins()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + (int)(score * 0.05f));
        UpdateUIText();
    }
    public void UseSkillCost(int cost)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) - cost);
        UpdateUIText();
    }
    public int GetScore()
    {
        return score;
    }
    public void SetHighScore()
    {
        HighScore.text = AdedZero(PlayerPrefs.GetInt("score", 0), 5);
    }
    public void SetPurchaseCoins(int number)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + number);
        UpdateUIText();
    }
    public bool UseSkill(int cost)
    {
        if(cost > PlayerPrefs.GetInt("coins", 0))
        {
            return false;
        }
        return true;
    }

}
