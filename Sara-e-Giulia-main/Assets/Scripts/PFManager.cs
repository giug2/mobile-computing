using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PFManager : MonoBehaviour
{

    [Header("UI")]
    public Text message;
    public InputField usernameInput;
    public InputField emailInputRegisterButton;
    public InputField passwordInputRegisterButton;
    public InputField emailInputLoginButton;
    public InputField passwordInputLoginButton;

    string Encrypt(string pass) {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString();
    }

    public void RegisterButton() {
        if (passwordInputRegisterButton.text.Length >= 6) {
            var request = new RegisterPlayFabUserRequest {
                Username = usernameInput.text,
                Email = emailInputRegisterButton.text,
                Password = Encrypt(passwordInputRegisterButton.text),
                RequireBothUsernameAndEmail = true
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
        else
        {
            message.text = "Password must be at least 6 characters long";
            return;
        }
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result) {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest{DisplayName = usernameInput.text},
            OnDisplayNameUpdate, OnError);
    }
    
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        message.text = "Account registered!";
    }
    
    public void LoginButton() {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInputLoginButton.text,
            Password = Encrypt(passwordInputLoginButton.text)
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result) {
        message.text = "Logged in!";
        SceneManager.LoadScene(1);
    }

    public void ResetPasswordButton() {
        var request = new SendAccountRecoveryEmailRequest {
            Email = emailInputLoginButton.text,
            TitleId = "BC97C"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result) {
        message.text = "Password reset email sent!";
    }

    bool getDeviceID(out string androidID, out string iOSID, out string customID) {
        androidID = string.Empty;
        iOSID = string.Empty;
        customID = string.Empty;
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass up = new AndroidJavaClass("com.unity2d.player.UnityPlayer");
            AndroidJavaObject objActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
            androidID = secure.CallStatic<string>("getString", objResolver, "android_id");
            return true;
        }
        //else 
        //if (Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //  iOSID = UnityEngine.iOS.Device.vendorIdentifier;
        //    return true;
        //}
        else
        {
            customID = SystemInfo.deviceUniqueIdentifier;
            return false;
        }
    }

    public void signInWithDevice() {
        if (getDeviceID(out string androidID, out string iOSID, out string customID))
        {
            if (!string.IsNullOrEmpty(androidID))
            {
                Debug.Log("Using Android Device ID: "+androidID);

                PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest()
                {
                    AndroidDeviceId = androidID,
                    TitleId = PlayFabSettings.TitleId,
                    CreateAccount = true
                }, OnAutomaticLoginSuccess, OnError);
            }
            else if (!string.IsNullOrEmpty(iOSID))
            {
                Debug.Log("Using IOS Device ID: "+ iOSID);

                PlayFabClientAPI.LoginWithIOSDeviceID(new LoginWithIOSDeviceIDRequest()
                {
                    DeviceId = iOSID,
                    TitleId = PlayFabSettings.TitleId,
                    CreateAccount = true
                }, OnAutomaticLoginSuccess, OnError);
            }
        }
        else
        {
            Debug.Log("Using custom device ID: "+customID);

            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
            {
                CustomId = customID,
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true
            }, OnAutomaticLoginSuccess, OnError);
        }
    }

    void OnAutomaticLoginSuccess(LoginResult result) {
        message.text = "Welcome!";
    }

    void OnError(PlayFabError error)
    {
        message.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
}
