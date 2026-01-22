using UnityEngine;
using UnityEngine.UI;

public class MainSettingsUIView : MonoBehaviour
{
    [SerializeField] private Button _buttonSettingsOpen;
    [SerializeField] private Button _buttonSettingsClose;
    [SerializeField] private Button _buttonGoHome;
    [SerializeField] private Button _buttonBackToGame;

    public void ChangeActive(bool isInGame)
    {
        ChangeActiveButtons(isInGame);

        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void ChangeActiveButtons(bool isInGame)
    {
        _buttonSettingsClose.gameObject.SetActive(!isInGame);
        _buttonGoHome.gameObject.SetActive(isInGame);
        _buttonBackToGame.gameObject.SetActive(isInGame);
    }
}
