using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Fusion.Photon.Realtime;

public class PlayerListController : PhotonListnerThree<PlayerStatus, PlayerRef, NetworkRunner> , IPlayerListController
{
    private IPlayerListPanel playerListPanel;
    public NetworkPrefabRef _roomPlayerPrefab;
    public List<RoomPlayer> Players = new List<RoomPlayer>();
    private bool IsAllReady() => Players.Count > 0 && Players.All(player => player.IsReady);

    protected override void Awake()
    {
        base.Awake();
        playerListPanel = GetComponent<IPlayerListPanel>();
        playerListPanel.Init(this);
        RoomPlayer.PlayerJoined += PlayerJoin;
    }

    private void OnDestroy()
    {
        RoomPlayer.PlayerJoined -= PlayerJoin;
    }

    public override void OnPhotonEventExecuted(PlayerStatus playerStatus, PlayerRef playerRef, NetworkRunner networkRunner)
    {
        switch (playerStatus)
        {

            case PlayerStatus.OPPONENT_JOINED:
                if (networkRunner.IsServer)
                {
                    var playerObject = networkRunner.Spawn(_roomPlayerPrefab, Vector3.zero, Quaternion.identity, playerRef);
                    networkRunner.SetPlayerObject(playerRef, playerObject);
                }
                break;
            case PlayerStatus.OPPONENT_LEFT:
                if (networkRunner.IsServer)
                {
                    OnPlayerLeft(networkRunner, playerRef);
                }
                break;
        }
    }


    private void PlayerJoin(RoomPlayer player)
    {
        Players.Add(player);
    }

    private void OnPlayerLeft(NetworkRunner networkRunner, PlayerRef playerRef)
    {
        var roomPlayer = Players.FirstOrDefault(x => x.Object.InputAuthority == playerRef);
        if (roomPlayer != null)
        {
            Players.Remove(roomPlayer);
            networkRunner.Despawn(roomPlayer.Object);
        }
    }

    public void PlayerReady(bool ready)
    {
       RoomPlayer.Local.RPC_SetReadyState(ready);
    }

    public void StartGame()
    {
        if (IsAllReady())
        {
            LoadSceneManager.Instance.LoadScene();
            
        }
        else
        {
            Debug.Log("All Player Not ready");
        }
    }
}
