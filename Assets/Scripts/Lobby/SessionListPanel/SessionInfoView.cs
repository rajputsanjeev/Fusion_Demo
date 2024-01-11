using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;

public class SessionInfoView : MonoBehaviour
{
    [SerializeField]private string roomKey;

    private ISessionList sessionList;
    public SessionInfo sessionInfo { set; get; }
    public Button joinSession;
    [SerializeField] private TMP_InputField roomCodeInputField;
    [SerializeField] public string sessionNameStr;
    [SerializeField] public string sessionPlayerCountStr;
    public TextMeshProUGUI sessionName;
    public TextMeshProUGUI playerJoinedSession;
    public TextMeshProUGUI maxPlayerJoinSession;

    public void SessionInfo(SessionInfo sessionInfo , ISessionList sessionList)
    {
        this.sessionList = sessionList;
        this.sessionInfo = sessionInfo;
        UpdateSessionInfo(sessionInfo);

        joinSession.gameObject.SetActive(sessionInfo.IsOpen && sessionInfo.PlayerCount < sessionInfo.MaxPlayers);

        SessionProperty keyProperty;
        sessionInfo.Properties.TryGetValue("key", out keyProperty);

        roomKey = string.Empty;

        if (keyProperty == null)
        {
            roomKey = string.Empty;
        }
        else
        {
            roomKey = keyProperty.PropertyValue as string;
        }
        Debug.Log("Key " + roomKey);
        roomCodeInputField.gameObject.SetActive(roomKey != string.Empty);

        joinSession.onClick.AddListener(JoinSession);
    }

    public void UpdateSessionInfo(SessionInfo sessionInfo)
    {
        this.sessionInfo = sessionInfo;
        sessionName.text = this.sessionInfo.Name;
        sessionNameStr = this.sessionInfo.Name;
        sessionPlayerCountStr = this.sessionInfo.PlayerCount.ToString();
        playerJoinedSession.text = this.sessionInfo.PlayerCount.ToString();
        maxPlayerJoinSession.text = this.sessionInfo.MaxPlayers.ToString();
    }

    public void OnDestroy()
    {
        joinSession.onClick.RemoveAllListeners();
    }

    private void JoinSession()
    {
        if(roomKey == "")
          sessionList.JoinSessiom(sessionInfo.Name);
        else
        {
            if(roomKey == roomCodeInputField.text)
            {
                sessionList.JoinSessiom(sessionInfo.Name);
            }
            else
            {
                Debug.Log("Room Code is Not Equal");
            }
        }
    }
}
