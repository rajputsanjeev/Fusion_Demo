using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public sealed class NetworkPlayer : NetworkBehaviour,IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }

    public override void Spawned()
    {

        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned local player");
            Camera.main.transform.GetComponent<CameraFollow>().target = transform;
        }

        else Debug.Log("Spawned remote player");
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)

            Runner.Despawn(Object);
    }
}
