using System;
using System.Collections;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UserGameState : IState
{
    [SerializeField] private float _timeToShowLose = 1.25f;
    [SerializeField] private float _timeToShowComputerAnswer = 0.5f;

    private const string CHEEKI = "Cheeki";
    private const string BREEKI = "Breeki";
    private const string CHEEKI_BREEKI = "CheekiBreeki";

    private GameController _owner;
    private GameModel _gameModelCore;
    private UIContainer _uiContainer;
    private LeaderboardController _leaderboardController;

    private uint _wins = 0;
    private uint _localNumber;
    private uint _minNumber = 1;
    private DateTime _startTime;
    private Guid id = Guid.NewGuid();

    private Coroutine _resultCoroutine;

    public UserGameState(GameController owner, GameModel gameModelCore,
                        UIContainer uiContainer, LeaderboardController leaderboardController)
    {
        _owner = owner;
        _gameModelCore = gameModelCore;
        _uiContainer = uiContainer;
        _leaderboardController = leaderboardController;

        CloseUI();
    }

    public EStateId StateID
    {
        get { return EStateId.game1; }
    }
    public void Enter()
    {
        StartGame();
    }
    public void Exit()
    {
        PostResults();
        CloseUI();
    }
    public void Update()
    {
    }

    private void StartGame()
    {
        ShowUI();
        Clear();

        _localNumber = _minNumber;
        _wins = 0;
        _startTime = DateTime.UtcNow;

        ShowNextQuestion();
    }

    private void UserCheeki()
    {
        Reply(CHEEKI);
    }
    private void UserBreeki()
    {
        Reply(BREEKI);
    }
    private void UserCheekiBreeki()
    {
        Reply(CHEEKI_BREEKI);
    }
    private void UserNumber()
    {
        Reply(_localNumber.ToString());
    }

    private void Reply(string userAnswer)
    {
        if (_resultCoroutine == null)
        {
            var data = _gameModelCore.GetGameData(_localNumber);

            _uiContainer.UserAnswerText.text = userAnswer;
            _resultCoroutine = _owner.StartCoroutine(CalculateResult(data, userAnswer));
        }
    }

    IEnumerator CalculateResult(GameData data, string userAnswer)
    {
        _uiContainer.ComputerAnswerText.text += string.Format("\n{0}", data.Line);
        if (userAnswer == data.Line)
        {
            yield return new WaitForSeconds(_timeToShowComputerAnswer);
            Win();
        }
        else
        {
            yield return new WaitForSeconds(_timeToShowComputerAnswer);
            Lose();
            yield return new WaitForSeconds(_timeToShowLose);
            StartGame();
        }

        _resultCoroutine = null;
    }

    private void Win()
    {
        _wins++;
        IncrementNumber();
        Clear();
        ShowNextQuestion();
    }

    private void ShowNextQuestion()
    {
        _uiContainer.ComputerAnswerText.text = string.Format("Number: {0}", _localNumber);
        _uiContainer.Btn_Number.GetComponentInChildren<TextMeshProUGUI>().text = _localNumber.ToString();
    }

    private void IncrementNumber()
    {
        if (_localNumber < uint.MaxValue)
        {
            _localNumber++;
        }
        else
        {
            _localNumber = _minNumber;
        }
    }

    private void Lose()
    {
        PostResults();
        Clear();
        ShowLose();
    }

    private void ShowLose()
    {
        _uiContainer.ComputerAnswerText.text = string.Format("Unfortunately you lose\nwins: {0}", _wins);
    }

    private void Clear()
    {
        _uiContainer.UserAnswerText.text = "";
        _uiContainer.ComputerAnswerText.text = "";
    }

    private void ShowUserAnswer(string userAnswer)
    {
        _uiContainer.UserAnswerText.text = userAnswer;
    }

    private void ShowUI()
    {
        _uiContainer.ComputerAnswerText.gameObject.SetActive(true);
        _uiContainer.UserAnswerText.gameObject.SetActive(true);

        _uiContainer.Btn_Cheeki.gameObject.SetActive(true);
        _uiContainer.Btn_Breeki.gameObject.SetActive(true);
        _uiContainer.Btn_CheekiBreeki.gameObject.SetActive(true);
        _uiContainer.Btn_Number.gameObject.SetActive(true);

        _uiContainer.Btn_Cheeki.onClick.AddListener(UserCheeki);
        _uiContainer.Btn_Breeki.onClick.AddListener(UserBreeki);
        _uiContainer.Btn_CheekiBreeki.onClick.AddListener(UserCheekiBreeki);
        _uiContainer.Btn_Number.onClick.AddListener(UserNumber);
    }

    private void CloseUI()
    {
        _uiContainer.ComputerAnswerText.gameObject.SetActive(false);
        _uiContainer.UserAnswerText.gameObject.SetActive(false);

        _uiContainer.Btn_Cheeki.gameObject.SetActive(false);
        _uiContainer.Btn_Breeki.gameObject.SetActive(false);
        _uiContainer.Btn_CheekiBreeki.gameObject.SetActive(false);
        _uiContainer.Btn_Number.gameObject.SetActive(false);

        _uiContainer.Btn_Cheeki.onClick.RemoveListener(UserCheeki);
        _uiContainer.Btn_Breeki.onClick.RemoveListener(UserBreeki);
        _uiContainer.Btn_CheekiBreeki.onClick.RemoveListener(UserCheekiBreeki);
        _uiContainer.Btn_Number.onClick.RemoveListener(UserNumber);
    }

    private void PostResults()
    {
        var endTime = DateTime.UtcNow;
        var resultDateTime = endTime - _startTime;
        float resultTime = (float)resultDateTime.TotalSeconds;
        PlayerResultModel result = new PlayerResultModel(id.ToString(), _wins, resultTime);
        _leaderboardController.PostResults(result, PostOK, PostFail);
    }

    private void PostOK()
    {
        Debug.Log("Post ok");
    }
    private void PostFail()
    {
        Debug.Log("Post fail");
    }
}
