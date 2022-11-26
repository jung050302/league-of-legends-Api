using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    public Text userName;
    public Text userLv;
    public Text connectionStatus;
    public Image connectionColor;
    public Toggle toggle;
    public Button btn;
    public static List<GameObject> del;

    public string puuid;
    public string id;
    public string name_;
    public string summonerlevel;

     

    private void Awake()
    {
     }
    private void Start()
    {
         


        del = new List<GameObject>();
    }
    
    public void Toggle_()
    {
        
        if (toggle.isOn)
        {

            del.Add(this.gameObject);
             
        }
        else if(!toggle.isOn)
        {
            
            del.Remove(this.gameObject);
             
        }
         
    }
    public void DelBtn()
    {
        
        for (int i = 0; i < del.Count; i++)
        {
            Destroy(del[i]);
            
        }
    }

    public void Btn()
    {
        PlayerPrefs.SetString("puuid", puuid);
        PlayerPrefs.SetString("id", id);
        PlayerPrefs.SetString("name",name_);
        PlayerPrefs.SetString("summonerLevel", summonerlevel);
        
        SceneManager.LoadScene("GameHistory");
    }


}
