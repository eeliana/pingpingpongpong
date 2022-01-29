using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public InputField createInput;
    public InputField joinInput;
    public InputField nicknameInput;

    // player limit
    const int MAX = 2;

    public void CreateRoom()
    {
        if (nicknameInput.text == "")
        {
            Debug.Log("Please enter a nickname.");
            return;
        }
        PhotonNetwork.NickName = nicknameInput.text;
        RoomOptions roomOptions = new RoomOptions() {MaxPlayers = MAX};
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom() 
    {

        if (nicknameInput.text == "")
        {
            Debug.Log("Please enter a nickname.");
            return;
        }
        PhotonNetwork.NickName = nicknameInput.text;
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("HoldingRoom");
    }
}
