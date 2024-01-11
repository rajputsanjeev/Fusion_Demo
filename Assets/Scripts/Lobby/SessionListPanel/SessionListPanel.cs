using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SessionListPanel : LobbyPanelBase , ISessionList                                                                                                                                               
{
    private ISessionListController sessionListController;

    [SerializeField] private Button createOpenRoomBtn;
    [SerializeField] private Button createPrivateRoomBtn;
    [SerializeField] private Button joinRandomRoomBtn;
    [SerializeField] private Button joinRoomWithNameBtn;
    [SerializeField] private RectTransform publicSessionConatiner;
    [SerializeField] private RectTransform privateSessionConatiner;
    [SerializeField] private SessionInfoView sessionInfoView;
    [SerializeField] Dictionary<string,SessionInfo> disSessions = new Dictionary<string, SessionInfo>();
    [SerializeField] private List<SessionInfoView> sessionInfoViews = new List<SessionInfoView>();
    public void Init(ISessionListController sessionListController)
    {
        this.sessionListController = sessionListController;
    }

    public override void InitPanel(LobbyUIManager lobbyUIManager)
    {
        base.InitPanel(lobbyUIManager);
        createOpenRoomBtn.onClick.AddListener(OnClickCreateOpenRoom);
        createPrivateRoomBtn.onClick.AddListener(OnClickCreatePrivateRoom);
        joinRandomRoomBtn.onClick.AddListener(OnClickJoinRandomRoom);
        joinRoomWithNameBtn.onClick.AddListener(OnClickJoinRoomWithName);
    }

    public void PhotonListnerData<T>(T sessions)
    {
        Debug.Log("PhotonListnerData Recieved");

        if(sessions.GetType() == typeof(List<SessionInfo>))
        {
            Debug.Log("Type Same ");

            List<SessionInfo> sessionInfos = sessions as List<SessionInfo>;

            foreach (var sessionView in sessionInfoViews.ToList())
            {
                if (!sessionInfos.Contains(sessionView.sessionInfo))
                {
                    disSessions.Remove(sessionView.sessionInfo.Name);
                    sessionInfoViews.Remove(sessionView);
                    DestroyImmediate(sessionView.gameObject);
                }
            }

            foreach (var session in sessionInfos)
            {
                if (disSessions.ContainsKey(session.Name))
                {
                    Debug.Log("Session Already Contain");
                    SessionInfoView sessionInfo = sessionInfoViews.Find(x => x.sessionNameStr == session.Name);
                    disSessions.Remove(session.Name);
                    DestroyImmediate(sessionInfo.gameObject);
                }
                disSessions.Add(session.Name, session);

                SessionProperty keyProperty;
                session.Properties.TryGetValue("key", out keyProperty);

                string key = "";    

                if(keyProperty == null)
                {
                    key = string.Empty;
                }else
                {
                    key = keyProperty.ToString();
                }

                if (key == null || key == ""|| key == string.Empty)
                {
                    SessionInfoView sessionInfo = Instantiate(sessionInfoView, publicSessionConatiner);
                    sessionInfoViews.Add(sessionInfo);
                    sessionInfo.SessionInfo(session, this);
                }
                else
                {
                    SessionInfoView sessionInfo = Instantiate(sessionInfoView, privateSessionConatiner);
                    sessionInfoViews.Add(sessionInfo);
                    sessionInfo.SessionInfo(session, this);
                }
            }
        }
    }

    public void JoinSessiom(string feildName)
    {
        sessionListController.JoinSessiom(feildName);
    }

    private void OnClickCreateOpenRoom()
    {
        lobbyUIManager.ShowPanel(LobbyPanelType.CreateOpenRoomPanel);
    }

    private void OnClickCreatePrivateRoom()
    {
        lobbyUIManager.ShowPanel(LobbyPanelType.CreatePrivateRoomPanel);
    }

    private void OnClickJoinRandomRoom()
    {
        lobbyUIManager.ShowPanel(LobbyPanelType.JoinRandomRoompanel);
    }

    private void OnClickJoinRoomWithName()
    {
        lobbyUIManager.ShowPanel(LobbyPanelType.JoinRoomWithNamePanel);
    }
}
