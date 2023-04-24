using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public class UIMediatorService : Singleton<UIMediatorService>
{
    CineCameraManagerService cineCameraManagerService => CineCameraManagerService.Instance;
    ZengaBlocksManagerService zengaBlocksManagerService => ZengaBlocksManagerService.Instance;

    GameObject gradeButtonPrefab => GlobalContext.Instance.gradeButtonPrefab;
    Transform gradeButtonParent => GlobalContext.Instance.gradeButtonParent;
    TMP_Text logStackText => GlobalContext.Instance.stackLogText;
    GameObject stackLogPanel => GlobalContext.Instance.stackLogPanel;

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
}
