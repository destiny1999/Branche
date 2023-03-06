using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace BrancheT5.Photon.Link
{ 
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        const string playerNamePrefKey = "PlayerName";

        void Start()
        {
            string defaultName = string.Empty;
            InputField _inputField =
                this.GetComponent<InputField>();
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.
                        GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }
            // 設定遊戲玩家的名稱
            
            PhotonNetwork.NickName = defaultName;
        }

        public void SetPlayerName(string value)
        {
            string n = this.GetComponent<InputField>().text;
            if (n.Equals(""))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            // 設定遊戲玩家的名稱
            else
            {
                PhotonNetwork.NickName = n;
                PlayerPrefs.SetString(playerNamePrefKey, n);
            }
            
        }
    }
}