using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SocialPlatforms;
using TMPro;

public class BlockBehaviour : NetworkBehaviour
{
    public TextMeshPro valueText;

    private int[] values = new int[1] {2};
    [Networked] public int value { get; set; }
    [Networked] public bool IsPlayerEnter { get; set; }
    
    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            value = values[Random.Range(0,values.Length)];
            valueText.text = value.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter ");

        if (IsPlayerEnter) return;

        if(other.gameObject.GetComponent<NetworkPlayer>().playerValue >= value)
        {
            other.gameObject.GetComponent<NetworkPlayer>().SetPlayerValue(value);
            Runner.Despawn(Object);
        }
    }
}
