using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndGame : MonoBehaviour
{
    public GameObject player;


    void Update()
    {
        if (gameObject.activeSelf == true)
        {

            
            // if (Input.GetKey(KeyCode.Space)){
            //     SaveGame();
            //     SceneManager.LoadScene(0);
            // }
                EndGame2();
        }


    }

    public void EndGame2(){
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Text>().text = "Skor: " +player.GetComponent<Player>().Score.ToString();
            transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = "Öldürme: " +player.GetComponent<Player>().totalKill.ToString();
            transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().text = GameData.SelectedLevel.ToString();
            transform.GetChild(0).GetChild(2).GetChild(GameData.SelectedClass).gameObject.SetActive(true);

    }


    public void Return(){
        SaveGame();
        SceneManager.LoadScene(0);
    }
    public void SaveGame()
    {
        if (GameData.HighScore < player.GetComponent<Player>().Score)
        {
            GameData.HighScore = player.GetComponent<Player>().Score;
            PlayerPrefs.SetInt("High Score", player.GetComponent<Player>().Score);
        }
        GameData.TotalKill += player.GetComponent<Player>().totalKill;
        PlayerPrefs.SetInt("Total Kill", GameData.TotalKill);
        PlayerPrefs.Save();

         
    }
}
