using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using YG;
using TMPro;
using UniRx;
using Zenject;
using Cysharp.Threading.Tasks;
using YG.LanguageLegacy;

public class TutorialService : BaseService
{
    [Header("Settings")]
    [SerializeField] private List<TutorialStep> _tutorialSteps;
    [SerializeField] private GameObject _parrotBlockViews;
    [SerializeField] private GameObject _palletViews;
    [SerializeField] private GameObject _crystallBagViews;
    [SerializeField] private GameObject _shipViews;
    [Header("UI")]
    [SerializeField] private Button _nextStep;
    [SerializeField] private TextMeshProUGUI _textStep;
    [SerializeField] private GameObject _panelTutorial;
    [SerializeField] private PanelAnimationView _panelTutorialTextAnimationView;

    private LangYGAdditionalText _additionalText;
    private LanguageYG _languageYG;

    [Inject]
    private SmoothLoaderService _smoothLoaderService;

    private TutorialPresenter _tutorialPresenter;

    public override void Initialize()
    {
        if (YG2.saves.isFirstGame == false)
            return;

        _additionalText = _textStep.GetComponent<LangYGAdditionalText>();
        _languageYG = _textStep.GetComponent<LanguageYG>();

        _tutorialPresenter = new TutorialPresenter(_tutorialSteps);

        _smoothLoaderService.LoadingCompletedCommand.Subscribe(_ =>
        {
            _tutorialPresenter.Initialize();
            SetActive(true);
            _panelTutorialTextAnimationView.Show();
            _tutorialPresenter.NextStep();
        });

        _tutorialPresenter.GoNextStepCommand.Subscribe(stepTutorial =>
        {
            DeselectAll();
            stepTutorial.TypeObjectSelectTutorial.ToList().ForEach(obj => SelectObjectTutorial(obj));
            UpdateTextStep(stepTutorial.TextStep);
        });

        _tutorialPresenter.FinishedTutorialCommand.Subscribe(async _ =>
        {
            DeselectAll();
            _panelTutorialTextAnimationView.Hide();
            await UniTask.Delay(1000);
            SetActive(false);
            YG2.saves.isFirstGame = false;
        });

        _nextStep.onClick.AddListener(() => _tutorialPresenter.NextStep());

    }

    public void SetActive(bool isActive)
        => _panelTutorial?.gameObject?.SetActive(isActive);

    private void UpdateTextStep(string textStep)
    {
        _languageYG.text = textStep;
        _languageYG.textMPComponent.SetText(textStep);
        ClearTraslation();
        _languageYG.AssignTranslate();
        _languageYG.Translate(_languageYG.countLang);
        _languageYG.AssignTranslate();
    }

    private void ClearTraslation()
    {
        for (int i = 0; i < _languageYG.languages.Length; i++)
            _languageYG.SetLang(i, "");
    }

    private void SelectObjectTutorial(TypeObjectSelectTutorial typeObjectSelectTutorial)
    {
        switch (typeObjectSelectTutorial)
        {
            case TypeObjectSelectTutorial.PalletViews:
                AddOutlineComponent(_palletViews);
                break;
            case TypeObjectSelectTutorial.ShipViews:
                AddOutlineComponent(_shipViews);
                break;
            case TypeObjectSelectTutorial.ParrotBlockViews:
                AddOutlineComponent(_parrotBlockViews);
                break;
            case TypeObjectSelectTutorial.CrystallBagViews:
                AddOutlineComponent(_crystallBagViews);
                break;
        }
    }

    private void DeselectAll()
    {
        OutlineNoActive(_parrotBlockViews);
        OutlineNoActive(_palletViews);
        OutlineNoActive(_crystallBagViews);
        OutlineNoActive(_shipViews);
    }

    private void OutlineNoActive(GameObject gameObject)
    {
        Outline outline = null;

        if (gameObject.TryGetComponent(out outline))
            outline.enabled = false;
    }

    private void AddOutlineComponent(GameObject gameObject)
    {
        Outline outline = null;

        if (gameObject.TryGetComponent(out outline))
        {
            outline.enabled = true;
            return;
        }

        var outlineComponent = gameObject.AddComponent<Outline>();
        outlineComponent.OutlineColor = new Color(0.1283184f, 1, 0, 1);
        outlineComponent.OutlineWidth = 6;
    }

    private void OnDestroy()
    {
        _nextStep.onClick.RemoveAllListeners();
    }
}
