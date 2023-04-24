using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GradeButton : MonoBehaviour
{
    int grade;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        //set button text as grade name getting it from zengablockmanagerservice
        button.GetComponentInChildren<TMP_Text>().text =
            ZengaBlocksManagerService.Instance.GetGradeName(grade);
    }

    private void Start()
    {
        button.onClick.AddListener(OnGradeSelect);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnGradeSelect);
    }

    public void Initialize(int id)
    {
        grade = id;
        gameObject.SetActive(true);
    }

    public void OnGradeSelect()
    {
        UIMediatorService.Instance.OnGradeSelect(grade);
    }
}
