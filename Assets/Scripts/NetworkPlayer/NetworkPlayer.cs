using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public sealed class NetworkPlayer : NetworkBehaviour,IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }

    public TextMeshPro valueText;
    [Networked] public int playerValue { get; private set; }
    private List<int> numbers = new List<int>();

    public override void Spawned()
    {
        Debug.Log("Player id " + Object.InputAuthority.PlayerId);
        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned local player");
            Camera.main.transform.GetComponent<CameraFollow>().target = transform;
        }
        else Debug.Log("Spawned remote player");
    }

    public void SetPlayerValue(int value)
    {
        numbers.Add(value);
        //playerValue = value;
        valueText.text = playerValue.ToString();
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
            Runner.Despawn(Object);
    }
}
