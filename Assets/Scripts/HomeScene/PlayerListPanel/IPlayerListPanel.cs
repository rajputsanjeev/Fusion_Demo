using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerListPanel
{
    void Init(IPlayerListController playerListController);
    void PhotonListnerData<T>(T data);
    void SetReady(bool ready);
}
