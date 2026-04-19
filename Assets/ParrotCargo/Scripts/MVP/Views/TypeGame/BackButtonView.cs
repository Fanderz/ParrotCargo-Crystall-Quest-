using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackButtonView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TypeGameUIView _typeGameUIView;
    [SerializeField] private LevelsView _levelsView;

    private Button _backButton;

    private void Awake()
    {
        _backButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveAllListeners();
    }

    private void OnBackButtonClicked()
    {
        if (_levelsView.gameObject.activeSelf)
            _levelsView.SetActive(false);
        else
            _typeGameUIView.SetActive(false);
    }

    private void OnValidate()
    {
        if(_backButton == null)
            _backButton = GetComponent<Button>();
    }
}
