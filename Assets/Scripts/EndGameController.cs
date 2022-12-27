using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameController : MonoBehaviour
{
    
    public TextMeshProUGUI title;
    public AchievementController achievementController;
    
    void Start()
    {
        string str;
        
        if(PlayerPrefs.GetInt("isMuseum") == 1){
            
            title.text = "Sveikiname perėjote muziejaus orentacinį žaidimą";
            str = "Muziejus end";
            
        }else{
            
            title.text = "Sveikiname perėjote renginio: \"" + PlayerPrefs.GetString("eventName") + "\" orentacinį žaidimą";
            str = PlayerPrefs.GetString("eventName") + " end";
            
        }
        
        achievementController.CallAddAchievements(str);
    }
    
    public void GoBack(){
        
        
        SceneManager.LoadScene("ChooseGameScene");   
    }
}
