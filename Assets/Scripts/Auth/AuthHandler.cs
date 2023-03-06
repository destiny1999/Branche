using System.Collections.Generic;
//using FullSerializer;
using Proyecto26;
using UnityEngine;
using TMPro;
using System.Collections;

public class AuthHandler : MonoBehaviour
{
    private const string apiKey = "AIzaSyCtxtdNO62HKKb85fDJRqSaoaUQ6QBoYUg"; // You can find this in your Firebase project settings

    //private static fsSerializer serializer = new fsSerializer();

    public delegate void EmailVerificationSuccess();
    public delegate void EmailVerificationFail();

    public static string idToken; // Key that proves the request is authenticated and the identity of the user
    public static string userId;

    public static AuthHandler Instance = null;

    [SerializeField] TMP_Text Email = null;
    [SerializeField] TMP_Text Pswd = null;

    [SerializeField] TMP_Text SignUpErrorMessage = null;
    [SerializeField] GameObject ErrorView = null;

    [SerializeField] GameObject WaitingView = null;

    /// <summary>
    /// Sings up user with Firebase Auth using Email and Password method
    /// Uploads the user object to Firebase Database
    /// Sends verification email
    /// </summary>
    /// <param name="email"> User's email </param>
    /// <param name="password"> User's password </param>
    /// <param name="user"> User object, which gets uploaded to Firebase Database </param>
    void Awake()
    {
        Instance = this;
    }

    public void CallGetData()
    {
        StartCoroutine(GetData());
    }
    public void HideWaiting()
    {
        WaitingView.SetActive(false);
    }
    public void ShowWaiting()
    {
        WaitingView.SetActive(true);
    }
    
    public IEnumerator GetData()
    {
        //print("Here");
        string e = Email.text;
        string p = Pswd.text;

        UserData user = new UserData();

        e = e.Substring(0, e.Length - 1);

        SignUp(e, p, user, SignUpErrorMessage, WaitingView, ErrorView);
        
        while (!waitMechanism.getOk())
        {
            yield return 0;
        }
        waitMechanism.SetOkToFalse();
        
        WaitingView.SetActive(false);
    }
    public void CallSignIn()
    {
        string e = Email.text;
        string p = Pswd.text;
        e = e.Substring(0, e.Length - 1);
        SignIn(e, p, WaitingView, ErrorView, SignUpErrorMessage);
    }
    public static void SignUp(string email, string password, UserData _regist, TMP_Text errorMessage, GameObject waitingView, GameObject errorView)
    {
        //print("CallSignUp");
        waitingView.SetActive(true);
        //The password should longer then 5
        var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
        RestClient.Post($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=AIzaSyDJmXQuoqlZWAIVPCUXviEhLALD8_Tj4VQ",
            payLoad).Then(
            response =>
            {
                //waitingView.SetActive(true);
                Debug.Log("SignUp success");

                var responseJson = response.Text;

                var data = JsonUtility.FromJson<UserData>(responseJson);
                UserData user = new UserData();
                user.idToken = data.idToken;
                DatabaseHandler.PostUser(user, data.localId);
                Debug.Log(data.idToken);
                waitMechanism.SetOkToTrue();
                //SendEmailVerification(data.idToken);
            }).Catch(errorCode => 
            {
                waitingView.SetActive(false);
                errorView.SetActive(true);
                if (password.Length < 6)
                {
                    errorMessage.text = "password shorter than 5";
                }
                else
                {
                    errorMessage.text = "please change your account";
                }
                print(errorMessage);
            });
    }

    /// <summary>
    /// Sends verification email
    /// </summary>
    /// <param name="newIdToken"> User's token, retrieved from SignUp </param>
    private static void SendEmailVerification(string newIdToken)
    {
        Debug.Log("Send email");
        var payLoad = $"{{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"{newIdToken}\"}}";
        RestClient.Post(
            $"https://www.googleapis.com/identitytoolkit/v3/relyingparty/getOobConfirmationCode?key=AIzaSyDJmXQuoqlZWAIVPCUXviEhLALD8_Tj4VQ", payLoad);
    }
    public static void SignIn(string email, string password, GameObject waitingView, GameObject errorView, TMP_Text errorMessage)
    {
        waitingView.SetActive(true);
        Debug.Log("Call sign in");
        var payLoad = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";
        RestClient.Post($"https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=AIzaSyDJmXQuoqlZWAIVPCUXviEhLALD8_Tj4VQ",
            payLoad).Then(
            response =>
            {
                
                var responseJson = response.Text;
                Debug.Log("respons = " + responseJson);
                var data = JsonUtility.FromJson<UserData>(responseJson);
                Debug.Log("Sign in success");
                User.Instance.userData = new UserData();
                User.login = true;
                User.Instance.userData.localId = data.localId;
                print(data.localId);
                //idToken = data.localId;
                DatabaseHandler.GetUserData(User.Instance.userData, User.Instance.userData.localId);
                DatabaseHandler.GetRecords(User.Instance, User.Instance.userData.localId);
                DatabaseHandler.GetGameStatusRecords(User.Instance, User.Instance.userData.localId);
                //waitingView.SetActive(false);
            }).Catch(errorCode =>
            {
                waitingView.SetActive(false);
                errorView.SetActive(true);
                errorMessage.text = "Account or password error.";
                //print(errorMessage);
            });
    }
    /*public void GetUserId()
    {
        HashSet<string> allUser = new HashSet<string>();
        DatabaseHandler.GetAllUserId(allUser);
        //DatabaseHandler.GetAllUserName(allName);
        StartCoroutine(GetUser(allUser));
        StartCoroutine(GetName(allName));
        //User.Instance.
    }*/
    
    /*IEnumerator GetUser(HashSet<string> allUser)
    {
        while (allUser.Count == 0)
        {
            yield return 0;
        }
        if (!allUser.Contains(tmp_playerName.text))
        {
            User.Instance.userData.name = tmp_playerName.text;
            Record[] records = User.Instance.getRecords();
            records = new Record[5];
            for (int i = 0; i < records.Length; i++)
            {
                records[i] = new Record();
            }
            //DatabaseHandler.UpdateUserAllData(User.Instance.userData, User.Instance.userData.localId, PostNewUserCompleted);
            DatabaseHandler.UpdateUserAllDataContainRecords(User.Instance.userData, User.Instance.userData.localId, PostNewUserCompleted);
            while (!PostNewUserCompleted)
            {
                yield return 0;
            }
            DatabaseHandler.GetUserData(User.Instance.userData, User.Instance.userData.localId);

            PlayerNameView.SetActive(false);

        }
        else
        {
            print("The name has been used");
        }
    }*/
}


