using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkRunnerControllerBase : MonoBehaviour
{
    public static NetworkRunnerControllerBase Instance { get; private set; }

    protected virtual void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public virtual async Task JoinLobby()
    {
    }

    protected void CallPhotonListner<T>(T data, NetworkRunner networkRunner = null)
    {
        PhotonListener<T,NetworkRunner>.Instance.OnPhotonEventExecuted(data, networkRunner);
    }

    protected void CallPhotonListner<T,T2>(T data,T2 data2, NetworkRunner networkRunner = null)
    {
        PhotonListnerThree<T,T2 , NetworkRunner>.Instance.OnPhotonEventExecuted(data,data2, networkRunner);
    }
}
