using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ContentThreadConnector : MonoBehaviour
{
    BackendConnector backendConnector;
    private void Start()
    {
        backendConnector = FindObjectOfType<BackendConnector>();
    }

    public async Task<List<ContentThread>> GetThreadsList()
    {
        List<ContentThread> threadList = await backendConnector.ServerGet<List<ContentThread>>("Start");
        
        return threadList;
    }

    //Здесь же POST, Create, Delete
}
