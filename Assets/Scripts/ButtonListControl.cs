using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    
    public GameObject emptyT;
    
    void Start(){
        
        StartCoroutine(GetAchievementData());
         
    }
    
    
    IEnumerator GetAchievementData(){
        
        WWWForm form = new WWWForm();
        
        form.AddField("username", DBManager.username);
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/getUserAchievements.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            string textLine = webRequest.downloadHandler.text;
            
            if(textLine != "Nėra"){
            
                string[] splited = textLine.Split(';');
                
                for(int i = 0; i < splited.Length-1; i++){
                
                    string name = splited[i].Split('\t')[1];
                    string url = splited[i].Split('\t')[2];
                    
                    GameObject button = Instantiate(buttonTemplate) as GameObject;
                    button.SetActive(true);
                    button.GetComponent<ButtonListButton>().SetParameters(url, name);
                    button.transform.SetParent(buttonTemplate.transform.parent, false);
                    
                } 
            }else{
                
                emptyT.SetActive(true);
            }
        }
        
    }
    
}
