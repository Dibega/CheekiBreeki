using TMPro;
using UnityEngine;

public class TextView : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI _text;
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void WriteNumberAndResult(uint number, string line)
    {
        _text.text = string.Format("Число: {0}\nРезультат: {1}", number, line);
    }

    public void Clear()
    {
        _text.text = "";
    }
}
