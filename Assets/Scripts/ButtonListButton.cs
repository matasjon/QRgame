using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mytext;
    
    [SerializeField]
    private RawImage myImage;
    
    public void SetParameters(string url ,string textString){
        
        mytext.text = textString;
        
        StartCoroutine(DownloadImage(url));
    }
    
    
    IEnumerator DownloadImage(string MediaUrl)
    {   
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl)){
            
            yield return request.SendWebRequest();
            
            if(request.isNetworkError || request.isHttpError) {
                
                Debug.Log(request.error);
            }
            else{
                myImage.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            }
        }
    } 
    // public void OnClick(){
        
    // }
}
