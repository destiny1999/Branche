using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;

namespace BrancheT5.Photon.Link
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [Tooltip("顯示/隱藏 遊戲玩家名稱與 Play 按鈕")]
        [SerializeField]
        private GameObject controlPanel = null;
        [Tooltip("顯示/隱藏 連線中 字串")]
        [SerializeField]
        private GameObject progressLabel = null;

        [Tooltip("遊戲室玩家人數上限. 當遊戲室玩家人數已滿額, 新玩家只能新開遊戲室來進行遊戲.")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        public GameObject PlayerName;

        // 遊戲版本的編碼, 可讓 Photon Server 做同款遊戲不同版本的區隔.
        string _gameVersion = "1";

        void Awake()
        {
            // 確保所有連線的玩家均載入相同的遊戲場景
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        // 與 Photon Cloud 連線
        public void Connect()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            // 檢查是否與 Photon Cloud 連線
            if (PhotonNetwork.IsConnected)
            {
                // 已連線, 嚐試隨機加入一個遊戲室
                //Debug.Log("TEST1");
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // 未連線, 嚐試與 Photon Cloud 連線
                PhotonNetwork.GameVersion = _gameVersion;
                PhotonNetwork.ConnectUsingSettings();
                //Debug.Log("TEST2");
            }
        }

        public override void OnConnectedToMaster()
        {
            //Debug.Log("Pun 呼叫 onConnectedToMaster(), 已連上 Photon Cloud.");

            //確認已連上 Photon Cloud
            //隨機加入一個遊戲室
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);

            Debug.LogWarningFormat("PUN 呼叫 OnDisconnected(){0}.", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN 呼叫 OnJoinRandomFailed(), 隨機加入遊戲室失敗.");

            //add new gameroom failed, because of 1. no room. 2. the esist all full
            //let's create by self
            PhotonNetwork.CreateRoom(null, new RoomOptions
            {
                MaxPlayers = maxPlayersPerRoom
            });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN 呼叫 OnJoinedRoom(), 已成功進入遊戲室中.");
            SceneManager.LoadScene("AllRoom");
        }

        public void Cancel()
        {
            SceneManager.LoadScene(1);
        }
    }
}