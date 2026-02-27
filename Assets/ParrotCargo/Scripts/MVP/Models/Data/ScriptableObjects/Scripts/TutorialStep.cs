using System.Collections.Generic;
using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "TutorialStep", menuName = "ScriptableObject/TutorialStep")]
public class TutorialStep : ScriptableObject
{
    [TextArea(1, 5)]
    [SerializeField] private string _ruTextStep;
    [TextArea(1, 5)]
    [SerializeField] private string _enTextStep;
    [TextArea(1, 5)]
    [SerializeField] private string _trTextStep;

    [SerializeField] private bool _isCompleted;

    [SerializeField] private List<TypeObjectSelectTutorial> _typeObjectSelectTutorial;

    //public string ruTextStep => _ruTextStep;
    public bool IsCompleted => _isCompleted;
    public IReadOnlyList<TypeObjectSelectTutorial> TypeObjectSelectTutorial => _typeObjectSelectTutorial;

    public void IsCompletedStep()
        => _isCompleted = true;

    public string GetCurrentTextStep()
    {
        if (YG2.lang == "en")
            return _enTextStep;
        else if(YG2.lang == "tr") 
            return _trTextStep;
        else
            return _ruTextStep;
    }
}
