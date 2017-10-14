using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loginManager : MonoBehaviour
{

    public GameObject email;
    public GameObject password;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Login()
    {
        StartCoroutine(loginEnumerator());
    }

    IEnumerator loginEnumerator()
    {
        byte[] b = Encoding.UTF8.GetBytes("email/password");
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add(email.GetComponentInChildren<Text>().text, password.GetComponentInChildren<Text>().text);
        WWW www = new WWW("http://ec7c612e.ngrok.io", b,headers);
        yield return www;
        if (www.error == null)
        {
            print("no problem");
            SceneManager.LoadScene("Main");
        }
        else
        {
            print(""+ www.error);
        }
    }
    public void Signin()
    {
        StartCoroutine(signInEnumerator());
    }
    IEnumerator signInEnumerator()
    {
        byte[] b = Encoding.UTF8.GetBytes("singin info");
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add(email.GetComponentInChildren<Text>().text, password.GetComponentInChildren<Text>().text);
        WWW www = new WWW("http://ec7c612e.ngrok.io", b, headers);
        yield return www;
        if (www.error == null)
        {
            print("no problem");
        }
        else
        {
            print("" + www.error);
        }
    }
}
