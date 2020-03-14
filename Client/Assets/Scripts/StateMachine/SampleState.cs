using System.Collections;
using UnityEngine;

[System.Serializable]
public class SampleState : IState
{
    [SerializeField] private TextView _textViewer;
    [SerializeField] private float _timeBeforeNextNumber = 0.5f;
    private float _firstTimeBeforeStart = 1f;
    private Coroutine sampleCoroutine;

    private GameController _owner;
    private GameModel _gameModelCore;
    private UIContainer _uiContainer;

    public SampleState(GameController owner, GameModel gameModelCore, UIContainer uiContainer)
    {
        _owner = owner;
        _gameModelCore = gameModelCore;
        _uiContainer = uiContainer;
        CloseUI();
    }

    public EStateId StateID
    {
        get { return EStateId.sample; }
    }

    public void Enter()
    {
        ShowUI();
        sampleCoroutine = _owner.StartCoroutine(Sample());
    }
    public void Exit()
    {
        _owner.StopCoroutine(sampleCoroutine);
        CloseUI();
    }
    public void Update()
    {
    }

    private IEnumerator Sample()
    {
        //yield return new WaitForSeconds(_firstTimeBeforeStart);
        TextView textView = _uiContainer.GameTextSample.GetComponent<TextView>();
        for (uint i = 1; i <= 100; i++)
        {
            GameData gameData = _gameModelCore.GetGameData(i);
            textView.WriteNumberAndResult(gameData.Number, gameData.Line);
            yield return new WaitForSeconds(_timeBeforeNextNumber);
        }
        textView.Clear();
    }

    private void ShowUI()
    {
        _uiContainer.GameTextSample.gameObject.SetActive(true);
    }

    private void CloseUI()
    {
        _uiContainer.GameTextSample.gameObject.SetActive(false);
    }
}
