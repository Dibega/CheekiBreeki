using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardViewController : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _resultPanelPrefab;

    private List<GameObject> _resultPanels = new List<GameObject>();

    public void ShowLeaderboard(LeaderboardModel leaderboardModel)
    {
        var playerResultModels = leaderboardModel.GetResults();
        foreach (var resultModel in playerResultModels)
        {
            var panel = Instantiate(_resultPanelPrefab, _content.transform);
            var lbPanel = panel.GetComponent<LeaderboardPanel>();

            lbPanel.TextID.text = resultModel.id;
            lbPanel.TextWins.text = resultModel.answerCount.ToString();
            lbPanel.TextTimes.text = TimeSpan.FromSeconds(resultModel.gameTime).ToString();

            _resultPanels.Add(panel);
        }
    }

    public void CloseLeaderboard()
    {
        for (int i = 0; i < _resultPanels.Count; i++)
        {
            Destroy(_resultPanels[i]);
        }
        gameObject.SetActive(false);
    }
}
