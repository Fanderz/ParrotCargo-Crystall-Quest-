using System.Collections.Generic;
using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "TutorialStep", menuName = "ScriptableObject/TutorialStep")]
public class TutorialStep : BaseLangTextSO
{
    [SerializeField] private bool _isCompleted;

    [SerializeField] private List<TypeObjectSelectTutorial> _typeObjectSelectTutorial;

    public bool IsCompleted => _isCompleted;
    public IReadOnlyList<TypeObjectSelectTutorial> TypeObjectSelectTutorial => _typeObjectSelectTutorial;

    public void IsCompletedStep()
        => _isCompleted = true;
}
