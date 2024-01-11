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
                    var playerObject = networkRunner.Spawn(NetworkPlayer, new Vector3(0, 50f, 0), Quaternion.identity, playerRef);
                    networkRunner.SetPlayerObject(playerRef, playerObject);
                }
                break;
        }
    }
}
