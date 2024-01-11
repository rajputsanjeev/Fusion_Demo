using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnService : PhotonListnerThree<PlayerStatus,PlayerRef, NetworkRunner>
{
    public NetworkPrefabRef NetworkPlayer;

    public override void OnPhotonEventExecuted(PlayerStatus data, PlayerRef playerRef, NetworkRunner networkRunner)
    {
        switch (data)
        {

            case PlayerStatus.OPPONENT_JOINED:
                if (networkRunner.IsServer)
                {
                    Debug.Log("Spawn");
                    var playerObject = networkRunner.Spawn(NetworkPlayer, new Vector3(Random.Range(-50,50), 100f, Random.Range(-50,50)), Quaternion.identity, playerRef);
                    networkRunner.SetPlayerObject(playerRef, playerObject);
                }
                break;
        }
    }
}
