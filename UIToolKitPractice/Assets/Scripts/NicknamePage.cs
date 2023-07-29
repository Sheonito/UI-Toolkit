using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class NicknamePage : MonoBehaviour
{
    private VisualElement popup;
    private Label title;
    private TextField textField;
    private Button createButton;

    private VisualElement root;

    private void Start()
    {
        UIDocument uiDoc = GetComponent<UIDocument>();
        root = uiDoc.rootVisualElement;

        popup = root.Q<VisualElement>("nicknamepopup");
        title = root.Q<Label>("Label");
        textField = root.Q<TextField>("TextField");
        createButton = root.Q<Button>("Button");

        StartCoroutine(AnimateUI());
    }

    private IEnumerator AnimateUI()
    {
        yield return new WaitUntil(() => popup.ClassListContains("nicknamepopup-scaledown"));
        yield return new WaitForSeconds(1);
        popup.RemoveFromClassList("nicknamepopup-scaledown");
        popup.RegisterCallback<TransitionEndEvent>(AnimateTitle);
    }

    private void AnimateTitle(TransitionEndEvent evt)
    {
        string titleMessage = "닉네임을 입력해주세요";
        DOTween.To(() => title.text, x => title.text = x, titleMessage, 2f)
            .SetEase(Ease.Linear)
            .onComplete += FadeInUI;
    }

    private void FadeInUI()
    {
        textField.RemoveFromClassList("textField-fadeout");
        createButton.RemoveFromClassList("button-fadeout");
    }
}