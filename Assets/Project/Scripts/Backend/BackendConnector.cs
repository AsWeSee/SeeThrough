using Microsoft.MixedReality.Toolkit.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


public class BackendConnector : MonoBehaviour
{

    public string serverurl = "https://localhost:44378/api/";
    // Start is called before the first frame update
    void Start()
    {

    }

    public async Task<T> ServerGet<T>(string url)
    {
        string json = await LoadTextFromServerAsync(url);
        ServiceResponse<T> responce = JsonConvert.DeserializeObject<ServiceResponse<T>>(json);
        return responce.Data;
    }

    public async Task<string> LoadTextFromServerAsync(string url)
    {
        string result;
        var request = UnityWebRequest.Get(serverurl + url);

        await request.SendWebRequest();


        if (!request.isHttpError && !request.isNetworkError)
        {
            result = request.downloadHandler.text;
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", "url", request.error);

            result = null;
        }
        request.Dispose();
        return result;
    }
}

