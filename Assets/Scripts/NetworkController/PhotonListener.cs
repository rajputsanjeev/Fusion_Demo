using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhotonListener<T1,NetworkRunner> : MonoBehaviour
{
    public static new PhotonListener<T1, NetworkRunner> Instance;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    public abstract void OnPhotonEventExecuted(T1 data, NetworkRunner networkRunner);
}
