using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListPanel : LobbyPanelBase, IPlayerListPanel
{
    private IPlayerListController playerListController;
    [SerializeField] private List<PlayerInfoView> playerInfoViewList = new List<PlayerInfoView>();
    [SerializeField] private PlayerInfoView playerInfoView;
    [SerializeField] private RectTransform parentContainer;
    [SerializeField] private Button startGameBtn;

    public void Init(IPlayerListController playerListController)
    {
        this.playerListController = playerListController;

        RoomPlayer.PlayerJoined += PlayerJoin;
        RoomPlayer.PlayerLeft += PlayerLeft;

        startGameBtn.onClick.AddListener(StartGame);
    }
    public override void InitPanel(LobbyUIManager lobbyUIManager)
    {
        base.InitPanel(lobbyUIManager);
    }

    public void PhotonListnerData<T>(T data)
    {
    }

    private void OnDestroy()
    {
        RoomPlayer.PlayerJoined -= PlayerJoin;
        RoomPlayer.PlayerLeft -= PlayerLeft;

        startGameBtn.onClick.RemoveAllListeners();
    }

    private void PlayerJoin(RoomPlayer roomPlayer)
    {
        Debug.Log("SpawnPlayerNamePlate "+ roomPlayer.playerName.Value);
        startGameBtn.gameObject.SetActive(roomPlayer.IsLeader);

        PlayerInfoView playerInfo = Instantiate(playerInfoView, parentContainer);
        playerInfoViewList.Add(playerInfo);
        playerInfo.SetPlayerName(roomPlayer.playerName.Value,roomPlayer, this);
    }

    private void PlayerLeft(RoomPlayer roomPlayerObject)
    {
        var roomPlayer = playerInfoViewList.FirstOrDefault(x => x.player.Object.InputAuthority == roomPlayerObject.Object.InputAuthority);
        if (roomPlayer != null)
        {
           playerInfoViewList.Remove(roomPlayer);
            DestroyImmediate(roomPlayer.gameObject,true);
        }
    }

    public void SetReady(bool ready)
    {
        playerListController.PlayerReady(true);
    }

    private void StartGame()
    {
        playerListController.StartGame();
    }
}
