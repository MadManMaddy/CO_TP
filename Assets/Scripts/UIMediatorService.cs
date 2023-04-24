using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public class UIMediatorService : Singleton<UIMediatorService>
{
    CineCameraManagerService cineCameraManagerService => CineCameraManagerService.Instance;
    ZengaBlocksManagerService zengaBlocksManagerService => ZengaBlocksManagerService.Instance;
    StacksFetchService stacksFetchService => StacksFetchService.Instance;

    GameObject gradeButtonPrefab => GlobalContext.Instance.gradeButtonPrefab;
    Transform gradeButtonParent => GlobalContext.Instance.gradeButtonParent;
    TMP_Text logStackText => GlobalContext.Instance.stackLogText;
    GameObject stackLogPanel => GlobalContext.Instance.stackLogPanel;
    GameObject refreshStacksButton => GlobalContext.Instance.refreshStacksButton;
    GameObject removeGlassStacksButton => GlobalContext.Instance.removeGlassStacksButton;

    private void Start()
    {
        zengaBlocksManagerService.OnZengaBlocksRefreshed += OnZengaBlocksRefreshed;
    }

    private void OnDestroy()
    {
        zengaBlocksManagerService.OnZengaBlocksRefreshed -= OnZengaBlocksRefreshed;
    }

    void OnZengaBlocksRefreshed()
    {
        for (int i = 0; i < zengaBlocksManagerService.zengaParentBlocks.Count; i++)
        {
            var gradeButton = Instantiate(gradeButtonPrefab, gradeButtonParent);
            gradeButton.GetComponent<GradeButton>().Initialize(i);
        }
        refreshStacksButton.SetActive(true);
        removeGlassStacksButton.SetActive(true);
    }

    public void OnGradeSelect(int grade)
    {
        cineCameraManagerService.SwitchCameraLookat(
            zengaBlocksManagerService.zengaParentBlocks[grade]
        );

        cineCameraManagerService.SetFreeLookCameraTargetOffset(
            zengaBlocksManagerService.zengaParentBlocks[grade].transform.childCount / 3
        );
    }

    public void OnStackClicked(Stack stack)
    {
        logStackText.text = JsonConvert.SerializeObject(stack);
        stackLogPanel.SetActive(true);
    }

    public void RefreshStacks()
    {
        RefreshUIButtons();
        refreshStacksButton.SetActive(false);
        removeGlassStacksButton.SetActive(false);
        stacksFetchService.FetchStacks();
    }

    public void RemoveGlassStacks()
    {
        removeGlassStacksButton.SetActive(false);
        zengaBlocksManagerService.RemoveGlassStacks();
    }

    void RefreshUIButtons()
    {
        foreach (Transform child in gradeButtonParent)
        {
            Destroy(child.gameObject);
        }
        logStackText.text = "";
        stackLogPanel.SetActive(false);
    }
}
