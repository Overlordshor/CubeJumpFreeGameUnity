using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    private int _count = 1;
    private Text _textLives;
    [SerializeField] private GameObject _endGameButtons;

    private CollisionHandler _collision;

    private void Start()
    {
        _textLives = GetComponent<Text>();
        Print();
    }

    private void OnEnable()
    {
        _textLives = GetComponent<Text>();
    }

    public void Add()
    {
        _count++;
        Print();
    }

    private void Remove()
    {
        _count--;
        Print();
    }

    private void UnscribeEvents()
    {
        _collision.OnFellGround -= Cube_OnFellGround;
        _collision.OnHitCube -= Cube_OnHitCube;
        _collision.OnJumped -= Cube_OnJumped;
    }

    private void DisplayEndGameButtons()
    {
        _endGameButtons.SetActive(true);
        _endGameButtons.transform.Find("AdvertisingButton").GetComponent<AdvertisingButton>().Display(AdvertisingType.ContinueReward);
    }

    public void Cube_OnCompressedCube()
    {
        throw new System.NotImplementedException();
    }

    public void Cube_OnFellGround()
    {
        Ended();

        UnscribeEvents();
    }

    public void Cube_OnHitCube()
    {
        Add();
        UnscribeEvents();
    }

    public void Cube_OnJumped()
    {
        Remove();
        _collision.OnJumped -= Cube_OnJumped;
    }

    public void SubscribeOnEvent()
    {
        _collision.OnFellGround += Cube_OnFellGround;
        _collision.OnHitCube += Cube_OnHitCube;
        _collision.OnJumped += Cube_OnJumped;
    }

    public void Print()
    {
        Language.PrintAnyLanguage(_textLives, $"Lives: {_count}", $"Жизней: {_count}");
    }

    public bool Ended()
    {
        if (_count == 0)
        {
            DisplayEndGameButtons();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Subcribe(CollisionHandler collision)
    {
        _collision = collision;
        SubscribeOnEvent();
    }
}