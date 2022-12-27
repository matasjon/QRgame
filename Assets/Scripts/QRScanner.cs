using System.Collections;
using System.Collections.Generic;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class QRScanner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private RawImage rawImageBackground;

    [SerializeField]
    private AspectRatioFitter aspectRatioFitter;

    [SerializeField]
    private TextMeshProUGUI textOut;

    [SerializeField]
    private RectTransform scanZone;

    [SerializeField]
    public GameObject nextButton;

    private bool isCamAvailable;
    private WebCamTexture cameraTexture;
    
    void Start()
    {
        if(PlayerPrefs.GetInt("isAnswered") == 1){
            
            nextButton.SetActive(true);
            
        }else{

            nextButton.SetActive(false);
        }
        
        SetUpCamera();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
    }

    private void SetUpCamera(){

        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0){
            isCamAvailable = false;
            return;
        }

        for(int i = 0; i < devices.Length; i++){
            if(devices[i].isFrontFacing == false){

                cameraTexture = new WebCamTexture(devices[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
            }
        }

        cameraTexture.Play();
        rawImageBackground.texture = cameraTexture;
        isCamAvailable = true;
    }

    private void UpdateCameraRender(){

        if(isCamAvailable == false){
            return;
        }

        float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
        aspectRatioFitter.aspectRatio = ratio;

        int orientation = -cameraTexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0 ,orientation);
    } 
    
    public void OnClickScan(){
        Scan();
    }
    
    private void Scan(){

        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);

            // int answr = int.Parse(result.Text);

            if(result != null){

                int answer = int.Parse(result.Text);
                
                if(PlayerPrefs.GetInt("scanEvent") == 0){
                    
                    int correct = PlayerPrefs.GetInt("QrCodeAnswer");
                    
                    if(answer == correct){

                        textOut.text = "Teisingas";
                        nextButton.SetActive(true);

                        PlayerPrefs.SetInt("isAnswered", 1);
                        StartCoroutine(SetSessionAnswer());
                        // StateNameController.isAswered = true;

                    }else{
                        textOut.text = "Neteisingas";
                    }
                    
                }else{
                    
                    PlayerPrefs.SetInt("EventQRcode", answer);
                    StartCoroutine(GetEventDetails());
                }
                
            }else{
                textOut.text = "Nepavyko nuskenuoti";
            }
        }
        catch
        {
            textOut.text = "Neteisingas";
        }
    }
    
    IEnumerator SetSessionAnswer(){
        
        WWWForm form = new WWWForm();
        
        form.AddField("answer", PlayerPrefs.GetInt("isAnswered").ToString());
        form.AddField("username", DBManager.username);
        
        string eventName = "";
        
        if(PlayerPrefs.GetInt("isMuseum") == 1){
            eventName = "Muziejus";
        }else{
            eventName = PlayerPrefs.GetString("eventName");
        }
        
        form.AddField("event", eventName);
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/updateSessionAnswer.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.downloadHandler.text != "0"){
    
                textOut.text = webRequest.downloadHandler.text;
                
            }
        }
        
    }
    
    IEnumerator GetEventDetails(){
        
        WWWForm form = new WWWForm();
        
        form.AddField("qrcode", PlayerPrefs.GetInt("EventQRcode").ToString());
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/getEventDetails.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.downloadHandler.text[0] == '0'){
                
                string eventName = webRequest.downloadHandler.text.Split('\t')[1];
                PlayerPrefs.SetString("eventName", eventName);
                
                // PlayerPrefs.SetInt("scanEvent", 0);
                textOut.text = "Renginys nuskenuotas";
                nextButton.SetActive(true);
               
                
            }else{
                PlayerPrefs.SetInt("EventQRcode", 0);
                textOut.text = webRequest.downloadHandler.text;
            }

        }
        
    }
    
    public void GoNext(){
        
        if(PlayerPrefs.GetInt("scanEvent") == 1){
            // PlayerPrefs.SetInt("scanEvent", 0);
            SceneManager.LoadScene("EventInfoScene");
            
        }else{
            
            SceneManager.LoadScene("QuestionAnswerScene");  
        }
    }
    
    public void GoBack(){
        
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene"));   
    }
}
