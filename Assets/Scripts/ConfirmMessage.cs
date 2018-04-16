using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Uses positive & negative as generic terms for 'yes&no', 'ok&cancel' etc
public class ConfirmMessage : MonoBehaviour {
    public event Action OnPositiveAction = null;
    public event Action OnNegativeAction = null;
    Button positiveButton;
    Button negativeButton;

    private void Start()
    {
        positiveButton = transform.Find("Positive Button").gameObject.GetComponent<Button>(); //Was going to be used for opening Dialog and Menu screens but no longer in use.
        positiveButton.onClick.AddListener(HandlePositiveClick);

        negativeButton = transform.Find("Negative Button").gameObject.GetComponent<Button>();
        negativeButton.onClick.AddListener(HandleNegativeClick);
    }

    public void SetMessage(string m)
    {
        transform.Find("Text").GetComponent<Text>().text = m;
    }

    private void HandlePositiveClick()
    {
        if (OnPositiveAction != null)
            OnPositiveAction();

        Hide();
    }

    private void HandleNegativeClick()
    {
        if (OnNegativeAction != null)
            OnNegativeAction();
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
