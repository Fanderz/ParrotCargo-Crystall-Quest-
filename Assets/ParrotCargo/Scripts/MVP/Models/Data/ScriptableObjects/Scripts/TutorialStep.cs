using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialStep", menuName = "ScriptableObject/TutorialStep")]
public class TutorialStep : ScriptableObject
{
    [TextArea(1, 5)]
    [SerializeField] private string _textStep;
    [SerializeField] private bool _isCompleted;
    [SerializeField] private List<TypeObjectSelectTutorial> _typeObjectSelectTutorial;

    public string TextStep => _textStep;
    public bool IsCompleted => _isCompleted;
    public IReadOnlyList<TypeObjectSelectTutorial> TypeObjectSelectTutorial => _typeObjectSelectTutorial;

    public void IsCompletedStep()
        => _isCompleted = true;
}
