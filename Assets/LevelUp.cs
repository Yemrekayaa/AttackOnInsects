using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public GameObject[] items;
     public GameObject[] UniqueItems;
    private GameObject[] itemList = new GameObject[3];


    private HashSet<GameObject> selectItem = new HashSet<GameObject>();
    void Start()
    {

    }

    void OnEnable()
    {
        GetComponent<AudioSource>().Play();
        if(GameObject.FindWithTag("Player").GetComponent<Player>().Level % 5 == 0)
            LoadCard(1);
        else
            LoadCard(0);
        Time.timeScale = 0;
    }


    void LoadCard(int u)

    {
        GameObject[] cards = new GameObject[3];
        cards[0] = transform.GetChild(0).GetChild(0).gameObject;
        cards[1] = transform.GetChild(0).GetChild(1).gameObject;
        cards[2] = transform.GetChild(0).GetChild(2).gameObject;

        while (selectItem.Count < 3)
        {
            if(u == 1)
                selectItem.Add(UniqueItems[Random.Range(0, UniqueItems.Length)]);
            else
                selectItem.Add(items[Random.Range(0, items.Length)]);
        }
        int i = 0;
        foreach (GameObject item in selectItem)
        {
            itemList[i] = item;
            if(u == 1){
                cards[i].GetComponent<Image>().color = new Color(0.23f,0f,0f,1f);
            }else{
                cards[i].GetComponent<Image>().color = new Color(0.443f, 0.415f, 0.415f,1f);;
            }
            cards[i].transform.GetChild(0).GetComponent<Text>().text = item.GetComponent<Drops>().itemName;
            cards[i].transform.GetChild(1).GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            cards[i].transform.GetChild(1).GetComponent<Image>().color = item.GetComponent<SpriteRenderer>().color;
            cards[i].transform.GetChild(2).GetComponent<Text>().text = item.GetComponent<Drops>().desc;
            i++;
        }
        selectItem.Clear();
    }


    public void SelectCard(int n)
    {
        Instantiate(itemList[n], GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
