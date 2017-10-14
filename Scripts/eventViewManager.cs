using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class eventViewManager : MonoBehaviour
{
    public GameObject text;
    public GameObject map;

    string exampleUrl =
            "https://maps.googleapis.com/maps/api/staticmap?center=Williamsburg,Brooklyn,NY&zoom=13&scale=1&size=400x400&markers=sdgaags&key=AIzaSyCf1Cw0y9TcnYkwuCINSrgzoVA6-0IqkvU"
        ;

    string key = "AIzaSyCf1Cw0y9TcnYkwuCINSrgzoVA6-0IqkvU"; //put your own API key here.

    IEnumerator getMap()
    {
        WWW www = new WWW(exampleUrl);
        yield return www;
        if (www.error == null)
        {
            map.GetComponent<Renderer>().material.mainTexture = www.texture;
            GameObject.Find("load").SetActive(false);
        }
        else
        {
            print(www.error);
        }
    }

    // Use this for initialization
    void Start()
    {
        text.transform.GetChild(0).GetComponentInChildren<Text>().text = "name " + PlayerPrefs.GetString("name");
        text.transform.GetChild(1).GetComponentInChildren<Text>().text = "by " + PlayerPrefs.GetString("by");
        text.transform.GetChild(2).GetComponentInChildren<Text>().text =
            "Description:\n" + PlayerPrefs.GetString("Description");
        text.transform.GetChild(3).GetComponentInChildren<Text>().text = "Place:\n" + PlayerPrefs.GetString("place");
        text.transform.GetChild(4).GetComponentInChildren<Text>().text = "Date:\n" + PlayerPrefs.GetString("date");
        text.transform.GetChild(5).GetComponentInChildren<Text>().text =
            "People Needed: " + PlayerPrefs.GetString("people");
        text.transform.GetChild(6).GetComponentInChildren<Text>().text =
            "Materiels Needed: " + PlayerPrefs.GetString("meteriel");
        StartCoroutine(getMap());
    }
    // Update is called once per frame
    void Update()
    {
    }
}