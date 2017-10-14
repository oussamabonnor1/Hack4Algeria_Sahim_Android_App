using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using LitJson;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class APIConsume : MonoBehaviour
{
    public GameObject EvenGameObject;
    public GameObject content;
    public GameObject searchField;

    private string jsonString;
    private static JsonData itemData;
    private static string newJsonFile;
    //Use this for initialization
    void Start()
    {
        StartCoroutine(WaitForRequest(new WWW("http://39781421.ngrok.io/api/events/latest")));
    }

    public static JsonData getJsonFile(string path)
    {
        string newPath = path.Replace(".json", "");
        TextAsset file = Resources.Load(newPath) as TextAsset;
        string jsonString = file.ToString();
        //string jsonString = File.ReadAllText(Application.dataPath + "/JSON files/" + path);
        return itemData = JsonMapper.ToObject(jsonString);
    }

    //path is original json,array is top of json file, needed neededURL is what u want from original json
    //this function gives back a needed json file
    public static JsonData getJsonByName(string path, string array)
    {
        JsonData itemData = getJsonFile(path);

        //for (int i = 0; i < itemData[array].Count; i++)
        {
            //return new Sheep().name = itemData["value"].ToString();
                
        }
        return null;
    }

    public static string getDataFromJson(JsonData json, string attribute)
    {
        
        return itemData[attribute].ToString();
    }

    public static JsonData getDataByIndex(string array, int index)
    {
        itemData = getJsonFile("sheepsDataBase.json");
        if (itemData[array][index] != null) return itemData[array][index]["url"];
        return null;
    }

    public void getDataFromServer()
    {
        StartCoroutine(WaitForRequest(new WWW("http://fde16c3f.ngrok.io/api/events/latest")));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            JsonData itemData = JsonMapper.ToObject(www.text);
            newJsonFile = www.text;
            print("");
            createEvents(itemData);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
            content.transform.GetChild(0).gameObject.SetActive(false);
            content.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    IEnumerator Post(string url)
    {
        PlayerPrefs.SetString("uid", "");
        byte[] b = Encoding.UTF8.GetBytes(url + PlayerPrefs.GetString("uid"));
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("type", "json");
        WWW www = new WWW("", b,headers);
        yield return www;
        if (www.error == null)
        {
            print("it worked :o");
        }
        else
        {
            print(www.error);
        }
    }

    public void createEvents(JsonData jsonData)
    {
        content.transform.GetChild(0).gameObject.SetActive(false);
        GameObject myEvent = Instantiate(EvenGameObject,content.transform);
        print(jsonData[0].ToJson());
        PlayerPrefs.SetString("description",jsonData[0]["_id"].ToString());
        PlayerPrefs.SetString("uid",jsonData[0]["uid"].ToString());
        //creating event
        myEvent.transform.GetChild(0).GetComponent<Text>().text = jsonData[0]["name"].ToString();
        myEvent.transform.GetChild(1).GetComponent<Text>().text = jsonData[0]["publisher"].ToString();
        //myEvent.transform.GetChild(2).GetComponent<Text>().text = jsonData[0]["participent"][0].ToString();
        //myEvent.transform.GetChild(3).GetComponent<Text>().text = jsonData[0]["needs"][0].ToString();
        //myEvent.transform.GetChild(4).GetComponent<Text>().text = jsonData[0]["place_map"].ToString();
        //myEvent.transform.GetChild(5).GetComponent<Text>().text = jsonData[0]["s_date"].ToString();
        myEvent.GetComponent<Button>().onClick.AddListener(openEvent);
        if(GameObject.Find("scroll")) GameObject.Find("scroll").GetComponent<Scrollbar>().value = 1;
        //StartCoroutine(Post());
    }

    public void openEvent()
    {
        PlayerPrefs.SetString("name",EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
        PlayerPrefs.SetString("by",EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Text>().text);
        PlayerPrefs.SetString("people",EventSystem.current.currentSelectedGameObject.transform.GetChild(2).GetComponentInChildren<Text>().text);
        PlayerPrefs.SetString("materiel",EventSystem.current.currentSelectedGameObject.transform.GetChild(3).GetComponentInChildren<Text>().text);
        PlayerPrefs.SetString("place",EventSystem.current.currentSelectedGameObject.transform.GetChild(4).GetComponentInChildren<Text>().text);
        PlayerPrefs.SetString("date",EventSystem.current.currentSelectedGameObject.transform.GetChild(5).GetComponentInChildren<Text>().text);
        SceneManager.LoadScene("eventView");
    }

    public void searchEvent()
    {
        string searchQuest = searchField.GetComponentsInChildren<Text>()[1].text;
        print(searchQuest);
        for (int i = 0; i < content.transform.childCount; i++)
        {
            if (!content.transform.GetChild(i).gameObject.name.Equals("load"))
            {
                //making sure we re not in the loader component
                if (!content.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text
                    .Contains(searchQuest))
                {
                    content.transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    content.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}

[Serializable]
public class Event
{
    public string _id;
    public string publisher;
    public string name;
    public string wilaya;
    public string[] pendingParticipents;
    public string[] participents;
    public string[] needs;
    public string founding;
    public string eventDescription;
    public string place_map;
    public string end_date;
    public string s_date;
    public string uid;
}
