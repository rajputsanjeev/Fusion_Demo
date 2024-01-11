using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneManager : NetworkSceneManagerDefault
{
   public static LoadSceneManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    protected override IEnumerator SwitchSceneSinglePeer(SceneRef prevScene, SceneRef newScene, FinishedLoadingDelegate finished)
    {
        return base.SwitchSceneSinglePeer(prevScene, newScene, finished);
    }

    public void SwitchScene(SceneRef prevScene, SceneRef newScene)
    {
        FinishedLoadingDelegate callback = (objects) =>
        {
            Debug.Log("Scene Loaded");
        };

        StartCoroutine(SwitchSceneSinglePeer(prevScene, newScene, callback));
    }

    public void LoadScene()
    {
        Debug.Log("Load Scene ");
        SwitchScene(1,2);
    }
}
