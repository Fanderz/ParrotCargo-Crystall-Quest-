using UnityEngine;
using UnityEngine.UI;

public class StartGameButtonView : MonoBehaviour
{
    [SerializeField] private Button _buttonStartGame;

    //public void InvokeButtonStartGame()
    //    => _buttonStartGame.onClick.Invoke();

    private void OnValidate()
    {
        if (_buttonStartGame == null)
            _buttonStartGame = GetComponent<Button>();
    }
}
