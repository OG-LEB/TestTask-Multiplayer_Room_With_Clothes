using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField CreateInput;
    [SerializeField] InputField JoinInput;

    public void CreateRoom() 
    {
        PhotonNetwork.CreateRoom(CreateInput.text);
    }
    public void JoinRoom() 
    {
        PhotonNetwork.JoinRoom(JoinInput.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GamePlay");
    }
}
