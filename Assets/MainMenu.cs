using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject dataPanel;
    public GameObject howToPlayPanel;
    public GameObject SelectLevel;
    public GameObject SelectClass;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void Start()
    {
        Time.timeScale = 1;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        GameControl();

    }
    private void GameControl()
    {
        if (!PlayerPrefs.HasKey("High Score"))
        {
            PlayerPrefs.SetInt("High Score", 0);
            PlayerPrefs.SetInt("Total Kill", 0);
            GameData.HighScore = 0;
            GameData.TotalKill = 0;

        }
        else
        {
            LoadGame();
        }

        dataPanel.transform.GetChild(0).GetComponentInChildren<Text>().text = "En Yüksek Skor: " + GameData.HighScore.ToString();
        dataPanel.transform.GetChild(1).GetComponentInChildren<Text>().text = "Toplam Öldürme: " + GameData.TotalKill.ToString();

        if (GameData.HighScore > 100)
        {
            SelectClass.transform.GetChild(1).GetComponent<Toggle>().interactable = true;
            SelectLevel.transform.GetChild(1).GetComponent<Toggle>().interactable = true;
        }
        if (GameData.HighScore > 200)
        {
            SelectClass.transform.GetChild(2).GetComponent<Toggle>().interactable = true;
            SelectLevel.transform.GetChild(2).GetComponent<Toggle>().interactable = true;
        }
        if (GameData.HighScore > 300)
        {
            SelectClass.transform.GetChild(3).GetComponent<Toggle>().interactable = true;
            SelectLevel.transform.GetChild(3).GetComponent<Toggle>().interactable = true;
        }
        if (GameData.HighScore > 400)
        {
            SelectClass.transform.GetChild(4).GetComponent<Toggle>().interactable = true;
            SelectLevel.transform.GetChild(4).GetComponent<Toggle>().interactable = true;
        }
        if (GameData.HighScore > 500)
        {
            SelectClass.transform.GetChild(5).GetComponent<Toggle>().interactable = true;
            SelectLevel.transform.GetChild(5).GetComponent<Toggle>().interactable = true;
        }
    }

    public void SelectedClass(int number)
    {
        GameData.SelectedClass = number;
        SelectClass.GetComponent<AudioSource>().Play();
    }

    public void SelectedLevel(int number)
    {
        GameData.SelectedLevel = number;
        SelectClass.GetComponent<AudioSource>().Play();
    }

    public void LoadGame()
    {
        GameData.HighScore = PlayerPrefs.GetInt("High Score");
        GameData.TotalKill = PlayerPrefs.GetInt("Total Kill");
        Debug.Log(GameData.HighScore);
    }


    public int Score
    {
        get
        {
            return Score;
        }
        set
        {
            Score = value;
        }
    }
    public void StartGame(GameObject panel)
    {

        StartCoroutine(StartGameDelay());
        ClosePanel(panel);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator StartGameDelay()
    {
        GetComponent<AudioSource>().Play();
        GameObject.Find("Dark").GetComponent<Image>().enabled = true;
        for (float i = 0; i < 2; i += Time.deltaTime)
        {
            GameObject.Find("Dark").GetComponent<Image>().color = new Color(0, 0, 0, Mathf.Clamp(i / 2, 0, 1f));
            GameObject.Find("Main Camera").GetComponent<AudioSource>().volume -= Time.deltaTime / 6.66f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        SceneManager.LoadScene(1);

    }
}
