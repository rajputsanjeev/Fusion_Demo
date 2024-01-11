using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomPlayer : NetworkBehaviour
{
    public static Action<RoomPlayer> PlayerJoined;
    public static Action<RoomPlayer> PlayerLeft;

    public static RoomPlayer Local;
    [Networked] public NetworkString<_32> playerName { set; get; }
    [Networked] public NetworkBool IsReady { get; set; }
    public bool IsLeader => Object != null && Object.IsValid && Object.HasStateAuthority;

    public override void Spawned()
    {
        base.Spawned();
        Debug.Log("Player id " + Object.InputAuthority.PlayerId);
        if (Object.HasInputAuthority)
        {
            Debug.Log("Object.HasInputAuthority ");
            Local = this;
            RPC_SetPlayerName(PlayerPrefs.GetString("PlayerName"));
        }
       
        PlayerJoined?.Invoke(this);
        DontDestroyOnLoad(gameObject);
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        base.Despawned(runner, hasState);
        PlayerLeft?.Invoke(this);
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority, InvokeResim = true)]
    public void RPC_SetPlayerName(NetworkString<_32> playerName)
    {
        Debug.Log("Set Player Name "+ playerName.Value);
        this.playerName = playerName;
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority, InvokeResim = true)]
    public void RPC_SetReadyState(NetworkBool ready)
    {
        this.IsReady = ready;
    }
}
