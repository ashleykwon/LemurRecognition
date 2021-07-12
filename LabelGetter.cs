using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Text;
using System.Linq;
using System;
using UnityEngine.XR.MagicLeap;

public class LabelGetter : MonoBehaviour
{
    public TextMeshProUGUI suggestedLabel1;
    public TextMeshProUGUI suggestedLabel2;
    public TextMeshProUGUI suggestedLabel3;
    public TextMeshProUGUI other;
    private readonly string url = "http://127.0.0.1:8000/predict";


    public class ImgPathData{
        public string image_path;
    }


    // Start is called before the first frame update
    void Start()
    {
    	string imagePath = "/Users/ashleykwon/Desktop/Summer_2021/lemurs_classified/val/coquerels-sifaka/Coquerel's_Sifaka,_Ankarafantsika,_Madagascar_(4027569042).jpg";
    	StartCoroutine(PostFilePath(imagePath));
    }


    IEnumerator PostFilePath(string imagePath) {
 		ImgPathData myObject = new ImgPathData();
        myObject.image_path = imagePath;

        string bodyJsonString = JsonUtility.ToJson(myObject);
        var request = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Network Error: " + request.error);
        }
        else if (request.isHttpError)
        {
            Debug.Log("Http Error: " + request.error);
        }
        else
        {
            Debug.Log("Image path uploaded successfully: " + request.downloadHandler.text);
            string labelsRaw = request.downloadHandler.text;
            string labelsEdited = labelsRaw.Replace('"',' ');
            string labelsEdited1 = labelsEdited.Replace("[", "");
            string labelsEdited2 = labelsEdited1.Replace("]", "");
            string labelsEdited3 = labelsEdited2.Replace(@"\", "");
            // string labelsEdited3 = labelsEdited2.Replace('\', ' ');
            
            string[] labels = labelsEdited3.Split(',');

            suggestedLabel1.text  = labels[0].Trim(' ');
            suggestedLabel2.text  = labels[1].Trim(' ');
            suggestedLabel3.text  = labels[2].Trim(' ');
            // Debug.Log(request.downloadHandler.data);
        }


    }

}
