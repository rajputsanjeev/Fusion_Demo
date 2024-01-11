using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInfoView : MonoBehaviour
{
    [SerializeField] private IPlayerListPanel playerListPanel;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI readyText;
    [SerializeField] private Button readyBtn;
    [SerializeField] public RoomPlayer player { set; get; }

    private void Awake()
    {
        readyBtn.onClick.AddListener(SetReadyBtn);    
    }

    private void OnDestroy()
    {
        readyBtn.onClick.RemoveAllListeners();
    }

    public void SetPlayerName(string playerName, RoomPlayer player ,IPlayerListPanel playerListPanel)
    {
        this.player = player;
        this.playerListPanel = playerListPanel;
        this.playerName.text = playerName;
        readyBtn.gameObject.SetActive(player.Object.HasInputAuthority);
    }

    public void SetReadyBtn() 
    {
        playerListPanel.SetReady(true);
    }

    private void Update() {
        if (player.Object != null && player.Object.IsValid)
        {
            playerName.text = player.playerName.Value;
            readyBtn.gameObject.SetActive(player.Object.HasInputAuthority && !player.IsReady);
            readyText.text = player.IsReady ? "Ready" : "Not Ready!";
        }
    }
}
