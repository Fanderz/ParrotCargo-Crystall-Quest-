using UnityEngine;
using YG;

public class BaseLangTextSO : ScriptableObject
{
    [TextArea(1, 5)]
    [SerializeField] protected string _ruText;
    [TextArea(1, 5)]
    [SerializeField] protected string _enText;
    [TextArea(1, 5)]
    [SerializeField] protected string _trText;

    public string GetTranslatedText()
    {
        if (YG2.lang == "en")
            return _enText;
        else if (YG2.lang == "tr")
            return _trText;
        else
            return _ruText;
    }
}
