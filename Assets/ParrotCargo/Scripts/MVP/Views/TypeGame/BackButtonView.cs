using UnityEngine;
using UnityEngine.UI;

public class BackButtonView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TypeGameUIView _typeGameUIView;
    [SerializeField] private LevelsView _levelsView;
    [Header("UI")]
    [SerializeField] private Button _back;

    private void Start()
    {
        _back.onClick.AddListener(OnBackButtonClicked);
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
        if(_back == null)
            _back = GetComponent<Button>();
    }
}
