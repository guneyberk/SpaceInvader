using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UImanager : MonoBehaviour
{
    private static UImanager instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public TextMeshProUGUI highscoreText;
    private int highscore = 0;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI waveText;
    private int wave;
    public Image[] lifeSprites;
    public Image healthBar;
    public Sprite[] healthBars;
    private Color32 active =  new Color(1,1,1,1);
    private Color32 inactive =  new Color(1,1,1,0.25f);
    private void Awake()
    {
        PlayerPrefs.SetFloat("highscore", 0);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public static void UpdateLives(int l)
    {
        foreach (Image i in instance.lifeSprites)
        {
            i.color = instance.inactive;
            
        }
        for (int i = 0; i < l; i++)
        {
            instance.lifeSprites[i].color = instance.active;
        }
    }
    public static void UpdateHealthbar(int h)
    {
        instance.healthBar.sprite = instance.healthBars[h];
    }
    public static void UpdateScore(int s)
    {
        instance.score += s;
        instance.scoreText.text = instance.score.ToString("000,000");
    }
    public static void UpdateHighScore()
    {
        if(instance.score > PlayerPrefs.GetFloat("highscore"))
        { 
            instance.highscore = PlayerPrefs.SetFloat("highscore",instance.score);
        }
       
    }
    public static void UpdateWave()
    {
        instance.wave++;
        instance.waveText.text = instance.wave.ToString();
    }
    public static void UpdateCoins()
    {
        instance.coinsText.text = Invertory.currentCoins.ToString();
    }
}
