using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUI : MonoBehaviour
{
    public GameObject player;
    public GameObject pHealth;
    public GameObject pEnergy;
    public GameObject pAttack;
    public GameObject pExp;
    public GameObject pExtra;
    public GameObject paused;
    public GameObject exit;
    public GameObject playerLevel;
    public Text time;
    public Text insects;
    public GameObject map;
    private float timeToDisplay = 0;

    public int guiMode = 0;

    private int maxEnemies;
    void Start()
    {
        StartCoroutine(StartGameDelay());
        playerLevel.transform.GetChild(0).GetChild(GameData.SelectedClass).gameObject.SetActive(true);
        maxEnemies = player.GetComponent<Spawn>().maxEnemies;
        Debug.Log(maxEnemies.ToString());
    }
    void Update()
    {
        if (GameObject.Find("LastDamage"))
            GameObject.Find("LastDamage").GetComponent<Text>().text = player.GetComponent<Player>().Score.ToString();
        pExtra.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Hareket Hızı=" + (Mathf.Round(((player.GetComponent<Player>().MSpeed) * 10.0f)) * 0.1f).ToString();
        pExtra.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "Şans =" + player.GetComponent<Player>().Luck.ToString();
        pExtra.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "Hasar Azaltma=" + (player.GetComponent<Player>().DamageReduction * 100).ToString() + "%";

        pExtra.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = "Can Çalma =" + (player.GetComponent<Player>().LifeSteal * 100).ToString() + "%";
        pExtra.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = "Enerji Çalma =" + (player.GetComponent<Player>().EnergySteal * 100).ToString() + "%";
        pExtra.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().text = "Ekstra Saldırı =" + player.GetComponent<Player>().ExtraAttack.ToString();
        insects.text = maxEnemies.ToString() + "/" + player.GetComponent<Player>().totalKill.ToString();
        playerLevel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = player.GetComponent<Player>().Level.ToString();

        pAttack.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "S.Hız = " + (Mathf.Round(((player.GetComponent<Player>().AttackSpeed / 60) * 10.0f)) * 0.1f).ToString();
        pAttack.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "S.Men = " + (Mathf.Round(((player.GetComponent<Player>().AttackRange) * 10.0f)) * 0.1f).ToString();
        pAttack.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "S.Has = " + player.GetComponent<Player>().DamageMin.ToString() + "-" + player.GetComponent<Player>().DamageMax.ToString();

        pHealth.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Sağ. = " + (Mathf.Round(((player.GetComponent<Player>().Health) * 10.0f)) * 0.1f).ToString();
        pHealth.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "S.Maks = " + player.GetComponent<Player>().MaxHealth.ToString();
        pHealth.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "S.Reg = " + player.GetComponent<Player>().HealthRegen.ToString();

        pEnergy.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Enrj. = " + (Mathf.Round(((player.GetComponent<Player>().Energy) * 10.0f)) * 0.1f).ToString();
        pEnergy.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "E.Maks = " + player.GetComponent<Player>().MaxEnergy.ToString();
        pEnergy.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = "E.Reg = " + player.GetComponent<Player>().EnergyRegen.ToString();

        //pExp.transform.GetChild(2).GetComponent<Text>().text = player.GetComponent<Player>().Exp.ToString() + "/" + player.GetComponent<Player>().MaxExp.ToString();

        if (Input.GetKeyDown(KeyCode.P) && exit.activeSelf == false)
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            paused.SetActive(Time.timeScale == 0 ? true : false);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioListener.volume = AudioListener.volume == 1 ? 0 : 1;

            GameObject.Find("Volume").GetComponent<Image>().enabled = (AudioListener.volume == 0 ? true : false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && paused.activeSelf == false)
        {
            exit.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (guiMode == 0)
            {
                pExtra.transform.parent.gameObject.SetActive(false);
                pExp.SetActive(false);
                guiMode = 1;
            }
            else if (guiMode == 1)
            {
                map.SetActive(false);
                insects.transform.parent.parent.gameObject.SetActive(false);
                guiMode = 2;
            }
            else if (guiMode == 2)
            {
                pExtra.transform.parent.gameObject.SetActive(true);
                pExp.SetActive(true);
                map.SetActive(true);
                insects.transform.parent.parent.gameObject.SetActive(true);
                guiMode = 0;
            }
        }


        TimeCounter();

    }

    public IEnumerator AttackCD()
    {
        Image playerAttackCD = pAttack.transform.GetChild(1).GetComponent<Image>();
        Image playerAttackCDChild = playerAttackCD.transform.GetChild(0).GetComponent<Image>();
        playerAttackCD.fillAmount = 0;
        playerAttackCDChild.color = new Color(0f, 0f, 0f, 1f);
        for (float i = 0; i <= (player.GetComponent<Player>().AttackSpeed / 60); i += Time.deltaTime)
        {
            playerAttackCD.fillAmount = Mathf.Clamp(i / (player.GetComponent<Player>().AttackSpeed / 60), 0, 1f);
            yield return new WaitForSeconds(Time.deltaTime);
            playerAttackCDChild.color = new Color(playerAttackCD.fillAmount, playerAttackCD.fillAmount, playerAttackCD.fillAmount, 1f);
        }
        playerAttackCD.fillAmount = 1;
        playerAttackCDChild.color = Color.white;
    }

    public void TimeCounter()
    {
        timeToDisplay += Time.deltaTime;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void UpdateHealthBar()
    {
        pHealth.transform.GetChild(1).GetComponent<Image>().fillAmount = Mathf.Clamp(player.GetComponent<Player>().Health / player.GetComponent<Player>().MaxHealth, 0, 1f);
    }

    public void UpdateExpBar()
    {
        pExp.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = Mathf.Clamp((float)player.GetComponent<Player>().Exp / (float)player.GetComponent<Player>().MaxExp, 0, 1f);

    }
    public void UpdateEnergyBar()
    {
        pEnergy.transform.GetChild(1).GetComponent<Image>().fillAmount = Mathf.Clamp(player.GetComponent<Player>().Energy / player.GetComponent<Player>().MaxEnergy, 0, 1f);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Cancel(GameObject go)
    {
        go.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator StartGameDelay()
    {
        GameObject.Find("Dark").GetComponent<Image>().enabled = true;
        for (float i = 0; i < 2; i += Time.deltaTime)
        {
            GameObject.Find("Dark").GetComponent<Image>().color -= new Color(0, 0, 0, Time.deltaTime / 2);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GameObject.Find("Dark").SetActive(false);
    }

}
