using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ServiceResponse<T>

{
    public T Data;
    public bool Success;
    public string Message;
}

public class ContentThread
{
    public long Id;

    public string name;

    public int models_count;

    public int total_models_size;

}

[Serializable]
public class MyClass
{
    public int level;
    public float timeElapsed;
    public string playerName;
}
public class BackendConnector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MyClass myObject = new MyClass();
        myObject.level = 1;
        myObject.timeElapsed = 47.5f;
        myObject.playerName = "Dr Charles Francis"; 
        string json = JsonUtility.ToJson(myObject);
        Debug.Log(json);

        MyClass  myObject2 = JsonUtility.FromJson<MyClass>(json);
        Debug.Log(myObject2.timeElapsed);
        // json now contains: '{"level":1,"timeElapsed":47.5,"playerName":"Dr Charles Francis"}'
    }

    public async void loadThreads()
    {
        Debug.Log("===========================");
        ContentThread contentThread = new ContentThread();
        contentThread.name = "asdasdsadasd";
        contentThread.total_models_size = 100;

        string json = JsonUtility.ToJson(contentThread);
        Debug.Log(json);

        ContentThread contentThread2 = JsonUtility.FromJson<ContentThread>(json);
        Debug.Log(contentThread2.name);
        Debug.Log(contentThread2.total_models_size);


        Debug.Log("===========================");

        string test = await LoadTextFromServerAsync();
        Debug.Log(test);
        ContentThread trans = JsonUtility.FromJson<ContentThread>(test);
        Debug.Log(trans.models_count);
        Debug.Log(trans.name);
    }
    public async Task<string> LoadTextFromServerAsync()
    {
        print("Load 1");
        string result;
        var request = UnityWebRequest.Get("https://localhost:44378/api/Start");

        await request.SendWebRequest();


        if (!request.isHttpError && !request.isNetworkError)
        {
            result = request.downloadHandler.text;
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", "yrl", request.error);

            result = null;
        }

        print("Load 2");
        request.Dispose();
        return result;
    }
    IEnumerator LoadTextFromServer(string url, Action<string> response)
    {


        print("Load 1");
        var request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (!request.isHttpError && !request.isNetworkError)
        {
            response(request.downloadHandler.text);
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);

            response(null);
        }

        print("Load 2");
        request.Dispose();
    }

}


public class UnityWebRequestAwaiter : INotifyCompletion
{
    private UnityWebRequestAsyncOperation asyncOp;
    private Action continuation;

    public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
    {
        this.asyncOp = asyncOp;
        asyncOp.completed += OnRequestCompleted;
    }

    public bool IsCompleted { get { return asyncOp.isDone; } }

    public void GetResult() { }

    public void OnCompleted(Action continuation)
    {
        this.continuation = continuation;
    }

    private void OnRequestCompleted(AsyncOperation obj)
    {
        continuation();
    }
}

public static class ExtensionMethods
{
    public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
    {
        return new UnityWebRequestAwaiter(asyncOp);
    }
}