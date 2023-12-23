using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime.Context.Game.Scripts.Models.Database
{
  public class DatabaseModel : IDatabaseModel
  {
    public IEnumerator SaveItem(string itemName, int amount)
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
  }
}