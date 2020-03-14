using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SampleState _sampleState;
    [SerializeField] private UserGameState _userGameState;
    [SerializeField] private UIContainer _uiContainer;
    [SerializeField] private LeaderboardController _leaderboardController;
    [SerializeField] private LeaderboardViewController _leaderboardViewController;

    private StateMachine _stateMachine = new StateMachine();
    private GameModel _gameModelCore = new GameModel();

    // Start is called before the first frame update
    void Start()
    {
        InitStateMachine();
        SetControllersBtns();
        ChangeToSampleState();
    }

    // Update is called once per frame
    void Update()
    {
        //_stateMachine.Update();
    }

    private void InitStateMachine()
    {
        _sampleState = new SampleState(this, _gameModelCore, _uiContainer);
        _userGameState = new UserGameState(this, _gameModelCore, _uiContainer, _leaderboardController);

        _stateMachine.AddState(_sampleState.StateID, _sampleState);
        _stateMachine.AddState(_userGameState.StateID, _userGameState);
    }

    private void ChangeToSampleState()
    {
        _stateMachine.ChangeState(EStateId.sample);
    }
    private void ChangeToUserGameState()
    {
        _stateMachine.ChangeState(EStateId.game1);
    }
    private void GetLeaderboard()
    {
        _leaderboardController.ShowLeaderboard(ShowLeaderboard, FailLeaderboard);
    }

    private void CloseLeaderboard()
    {
        _leaderboardViewController.CloseLeaderboard();
    }

    private void SetControllersBtns()
    {
        _uiContainer.Btn_SampleModeStart.onClick.AddListener(ChangeToSampleState);
        _uiContainer.Btn_UserModeStart.onClick.AddListener(ChangeToUserGameState);
        _uiContainer.Btn_Leaderboard.onClick.AddListener(GetLeaderboard);
        _uiContainer.Btn_CloseLeaderboard.onClick.AddListener(CloseLeaderboard);
    }

    private void ShowLeaderboard()
    {
        _leaderboardViewController.gameObject.SetActive(true);
        _leaderboardViewController.ShowLeaderboard(_leaderboardController.LeaderboardModel);
    }

    private void FailLeaderboard()
    {
        Debug.Log("Error data");
    }
}
