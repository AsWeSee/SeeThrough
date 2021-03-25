using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentThreadsListUI : MonoBehaviour
{

    public ContentThreadUI contentThreadUI;
    public Transform contentThreadsListContainer;

    ContentThreadConnector contentThreadConnector;
    private void Start()
    {
        contentThreadConnector = FindObjectOfType<ContentThreadConnector>();
    }
    public async void UpdateThreadsList()
    {
        List<ContentThread> contentThreads = await contentThreadConnector.GetThreadsList();

        foreach (GameObject gameObject in contentThreadsListContainer)
        {
            Destroy(gameObject);
        }

        foreach (ContentThread contentThread in contentThreads)
        {
            ContentThreadUI instance = Instantiate(contentThreadUI, contentThreadsListContainer);
            instance.Fill(contentThread);
        }
    }
}
