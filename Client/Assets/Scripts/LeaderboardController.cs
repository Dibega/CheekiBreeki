using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _errorText;
    [SerializeField] private float timeToShowError = 0.8f;

    public LeaderboardModel LeaderboardModel { get; private set; }
    private const string _serverPath = "http://localhost";
    private const int _port = 5000;

    private Coroutine _getLeaderboardCoroutine;
    private Coroutine _postResultsCoroutine;


    public void ShowLeaderboard(Action onOk, Action onFail)
    {
        if (_getLeaderboardCoroutine == null)
            _getLeaderboardCoroutine = StartCoroutine(GetLeaderboard(onOk, onFail));
    }

    public void PostResults(PlayerResultModel result, Action onOk, Action onFail)
    {
        if (_postResultsCoroutine == null)
            _postResultsCoroutine = StartCoroutine(PostResult(result, onOk, onFail));
    }

    private IEnumerator GetLeaderboard(Action onOk, Action onFail)
    {
        UnityWebRequest www = UnityWebRequest.Get(string.Format("{0}:{1}/api/leaderboard", _serverPath, _port));
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.responseCode != 200)
        {
            StartCoroutine(ShowError(www.error));
            onFail();
        }
        else
        {
            Debug.Log(www.responseCode);
            Debug.Log(www.downloadHandler.text);
            LeaderboardDto lbDto = JsonUtility.FromJson<LeaderboardDto>(www.downloadHandler.text);
            LeaderboardModel = new LeaderboardModel(lbDto);
            onOk();
        }

        _getLeaderboardCoroutine = null;
    }

    private IEnumerator PostResult(PlayerResultModel result, Action onOk, Action onFail)
    {
        var json = JsonUtility.ToJson(result);
        Debug.Log(json);
        byte[] bJson = Encoding.UTF8.GetBytes(json);
        UnityWebRequest www = UnityWebRequest.Post(string.Format("{0}:{1}/api/leaderboard", _serverPath, _port), string.Empty);
        www.uploadHandler = new UploadHandlerRaw(bJson);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.responseCode != 200)
        {
            StartCoroutine(ShowError(www.error));
            onFail();
        }
        else
        {
            Debug.Log(www.responseCode);
            onOk();
        }

        _postResultsCoroutine = null;
    }

    private IEnumerator ShowError(string error)
    {
        Debug.Log(error);
        _errorText.text = error;
        yield return new WaitForSeconds(timeToShowError);
        _errorText.text = "";
    }
}
