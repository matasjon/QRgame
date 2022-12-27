using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPageController : MonoBehaviour
{
    
    
    public void GoToGameChoose(){

        SceneManager.LoadScene("ChooseGameScene");
    }
    
    public void GoToAchievements(){
        

        SceneManager.LoadScene("Achievments");
    }
    
    public void GoToRules(){
        

        SceneManager.LoadScene("Rules");
    }
    
    public void GoToOptions(){
        

        SceneManager.LoadScene("OptionsScene");
    }
}
