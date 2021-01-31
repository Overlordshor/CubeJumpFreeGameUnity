using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    private int _count = 1;
    private Text _textLives;

    private void Start()
    {
        _textLives = GetComponent<Text>();
        Print();
    }

    public void Print()
    {
        Language.PrintAnyLanguage(_textLives, $"Lives: {_count}", $"Жизней: {_count}");
    }

    public void Add()
    {
        _count++;
        Print();
    }

    public void Remove()
    {
        _count--;
        Print();
    }
}