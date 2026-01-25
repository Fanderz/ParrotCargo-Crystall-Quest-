using UnityEngine;

[CreateAssetMenu(fileName = "TutorialStep", menuName = "ScriptableObject/TutorialStep")]
public class TutorialStep : ScriptableObject
{
    [SerializeField] private string _textStep;
    [SerializeField] private bool _isCompleted;
    [SerializeField] private TypeObjectSelectTutorial _typeObjectSelectTutorial;

    public string TextStep => _textStep;
    public bool IsCompleted => _isCompleted;
    public TypeObjectSelectTutorial TypeObjectSelectTutorial => _typeObjectSelectTutorial;

    public void IsCompletedStep()
        => _isCompleted = true;
}
