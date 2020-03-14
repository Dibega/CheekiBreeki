using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIContainer : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI GameTextSample;
    [SerializeField] public TextMeshProUGUI UserAnswerText;
    [SerializeField] public TextMeshProUGUI ComputerAnswerText;

    [SerializeField] public Button Btn_SampleModeStart;
    [SerializeField] public Button Btn_UserModeStart;
    [SerializeField] public Button Btn_Leaderboard;
    [SerializeField] public Button Btn_CloseLeaderboard;

    [SerializeField] public Button Btn_Cheeki;
    [SerializeField] public Button Btn_Breeki;
    [SerializeField] public Button Btn_CheekiBreeki;
    [SerializeField] public Button Btn_Number;
}
