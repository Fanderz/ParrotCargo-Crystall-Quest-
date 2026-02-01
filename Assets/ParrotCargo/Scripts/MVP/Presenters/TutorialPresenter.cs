using System.Collections.Generic;
using UnityEngine;

using UniRx;
using YG;

public class TutorialPresenter
{
    private List<TutorialStep> _tutorialSteps;
    private int _currentIndexStep;

    public TutorialPresenter(List<TutorialStep> tutorialSteps)
    {
        _tutorialSteps = tutorialSteps;
    }

    public ReactiveCommand<TutorialStep> GoNextStepCommand = new();
    public ReactiveCommand FinishedTutorialCommand = new();

    public void Initialize()
    {
        _currentIndexStep = 0;
    }

    public void NextStep()
    {
        if (_currentIndexStep >= _tutorialSteps.Count)
        {
            FinishedTutorialCommand.Execute();
            return;
        }

        _tutorialSteps[_currentIndexStep].IsCompletedStep();
        var currentStep = _tutorialSteps[_currentIndexStep];
        ++_currentIndexStep;

        GoNextStepCommand.Execute(currentStep);
    }
}
