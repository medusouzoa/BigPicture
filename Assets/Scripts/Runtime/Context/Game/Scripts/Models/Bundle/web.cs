using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime.Context.Game.Scripts.Models.Bundle
{
  public class web : MonoBehaviour
  {
    void Start()
    {
      //StartCoroutine(GetRequest("http://localhost/bigPictureDb/bigPicture.php"));
      //StartCoroutine(SaveItem("wrench", 3));
      //StartCoroutine(GetUserItems());
    }

    public static IEnumerator SaveItem(string itemName, int amount)
    {
      WWWForm form = new WWWForm();
      form.AddField("name", itemName);
      form.AddField("amount", amount);
      using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/bigPictureDb/SaveInventory.php", form))
      {
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
          Debug.Log(www.error);
        }
        else
        {
          Debug.Log(www.downloadHandler.text);
        }
      }
    }

    public static IEnumerator UpdateAmount(string itemName, int newAmount)
    {
      WWWForm form = new WWWForm();
      form.AddField("name", itemName);
      form.AddField("amount", newAmount);

      using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/bigPictureDb/inventoryUpdate.php", form))
      {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
          Debug.Log(www.error);
        }
        else
        {
          Debug.Log(www.downloadHandler.text);
        }
      }
    }

    public static IEnumerator UpdateMoneyAmount(int id, int newAmount)
    {
      WWWForm form = new WWWForm();
      form.AddField("id", id);
      form.AddField("amount", newAmount);

      using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/bigPictureDb/moneyUpdate.php", form))
      {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
          Debug.Log(www.error);
        }
        else
        {
          Debug.Log(www.downloadHandler.text);
        }
      }
    }

    public static IEnumerator GetMoneyAmountCoroutine(System.Action<string> callback)
    {
      WWWForm form = new WWWForm();
      form.AddField("id", 0);

      using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/bigPictureDb/GetMoneyAmount.php", form))
      {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
          Debug.Log(www.error);
        }
        else
        {
          Debug.Log(www.downloadHandler.text);
          string itemsJson = www.downloadHandler.text;
          callback(itemsJson);
        }
      }
    }

    public static IEnumerator GetUserItems(System.Action<string> callback)
    {
      using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/bigPictureDb/GetInventory.php"))
      {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
          Debug.Log(www.error);
        }
        else
        {
          Debug.Log(www.downloadHandler.text);
          string itemsJson = www.downloadHandler.text;
          callback(itemsJson);
        }
      }
    }

      
    IEnumerator GetRequest(string uri)
    {
      using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
      {
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        switch (webRequest.result)
        {
          case UnityWebRequest.Result.ConnectionError:
          case UnityWebRequest.Result.DataProcessingError:
            Debug.LogError(pages[page] + ": Error: " + webRequest.error);
            break;
          case UnityWebRequest.Result.ProtocolError:
            Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
            break;
          case UnityWebRequest.Result.Success:
            Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            break;
        }
      }
    }
  }
}