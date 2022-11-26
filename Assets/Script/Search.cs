using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
public class Search : MonoBehaviour
{
    public InputField input;
    public GameObject content;
    public UserManager user_box;
    UserInfo info;
    

    public string puuid;

    
     

    // Update is called once per frame
    void Update()
    {
        
         
        if (input.text.Length != 0 && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(Search_("api"));
            

        }
    }

    IEnumerator Search_(string api)
    {

        UnityWebRequest request = UnityWebRequest.Get(api);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            info = JsonUtility.FromJson<UserInfo>(request.downloadHandler.text);
            UserManager userBox = Instantiate(user_box, content.transform);
            userBox.userName.text = info.name;
            userBox.userLv.text = info.summonerLevel+"LV";
             input.text = "";
             userBox.puuid = info.puuid;
            userBox.name_ = info.name;
            userBox.summonerlevel = info.summonerLevel;
            puuid = info.puuid;
         }
    }

     

    

}

[SerializeField]
public  class UserInfo
{
    public string id;
    public  string accountId;
    public string puuid;
    public  string name;
    public  string profileIconId;
    public  string revisionDate;
    public  string summonerLevel;

}
