using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class GameHistory : MonoBehaviour
{
    string matchId;
    public string[] matchIdLst;
    public HistoryBox hisBox;
    public GameObject content;
    private void Awake()
    {
        StartCoroutine(MatcheListSearch(string.Format("api")));
         
    }
     

    IEnumerator MatcheListSearch(string api)
    {
        print(api);
        UnityWebRequest request = UnityWebRequest.Get(api);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
             
            for (int i = 0; i < request.downloadHandler.text.Length; i++)
            {
                if (request.downloadHandler.text[i] == '[' || request.downloadHandler.text[i] == ']' || request.downloadHandler.text[i] == '"')
                {
                    continue;
                }
                else
                {
                    matchId += request.downloadHandler.text[i];
                }
            }
            matchIdLst = matchId.Split(',');
            int b;
            for (int i = 0; i < matchIdLst.Length; i++)
            {
                 
                Thread.Sleep(250);
                StartCoroutine(MatcheSearch(string.Format("api")));
                
            }
             
            Debug.Log(request.downloadHandler.text);
        }
    }



    IEnumerator MatcheSearch(string api)
    {
        //print(api);
        UnityWebRequest request = UnityWebRequest.Get(api);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            data json = JsonUtility.FromJson<data>(request.downloadHandler.text);
            
            Debug.Log(request.downloadHandler.text);

            for (int i = 0; i < json.info.participants.Length; i++)
            {
                if (json.info.participants[i].summonerName == PlayerPrefs.GetString("name"))
                {

                    print(string.Format("mode: {0}", json.info.gameMode));
                    print(string.Format("Name: {0}", json.info.participants[i].championName));
                    print(string.Format("kills: {0}", json.info.participants[i].kills));
                    print(string.Format("deaths: {0}", json.info.participants[i].deaths));
                    print(string.Format("win: {0}", json.info.participants[i].win));
                    print(string.Format("as: {0}", json.info.participants[i].assists));
                    print(string.Format("username: {0}", json.info.participants[i].summonerName));
                    print(string.Format("matId: {0}", json.metadata.matchId));

                    HistoryBox his = Instantiate(hisBox, content.transform);
                    his.championName.text = json.info.participants[i].championName;
                    his.championLv.text = json.info.participants[i].champLevel;
                    his.kda.text = string.Format("{0}/{1}/{2}",json.info.participants[i].kills, json.info.participants[i].deaths, json.info.participants[i].assists);
                    his.result.text = (json.info.participants[i].win == "true" ? "win" : "lose");
                    his.gameMode.text = json.info.gameMode;
                }


            }

        }
    }



}



[System.Serializable]
public class data
{
    public Data3 metadata;
    public Data info;
     
    //info
}


[System.Serializable]
public class Data
{
    public string gameId;
    public string gameMode;
    public Data2[] participants;
    //public string championName;
    //public string kills;
    //public string deaths;
    //public string win;
    //public string assists;

}


[System.Serializable]
public class Data2
{
    public string championName;
    public string champLevel;
    public string kills;
    public string assists;
    public string deaths;
    public string win;
    public string summonerName;
}


 

[System.Serializable]
public class Data3
{
    public string matchId;
}