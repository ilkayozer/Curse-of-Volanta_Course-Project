using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    private int GemScore;
    public Text GemText;

    private void Awake()
    {
        GemScore = PlayerPrefs.GetInt("Gems");
    }

    private void Start()
    {
        GemText.text ="X "+ GemScore.ToString();
    }
   

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
