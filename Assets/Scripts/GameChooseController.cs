using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChooseController : MonoBehaviour
{
    
    public void GoToMuseum(){
        // PlayerPrefs.SetString("PreviousScene", "ChooseGameScene");
        PlayerPrefs.SetInt("isMuseum", 1);
        SceneManager.LoadScene("QuestionScene");   
    }
    
    public void GoToEvent(){
        
        // PlayerPrefs.SetString("PreviousScene", "ChooseGameScene");
        PlayerPrefs.SetInt("isMuseum", 0);
        SceneManager.LoadScene("ChooseEventScene");   
    }
}
