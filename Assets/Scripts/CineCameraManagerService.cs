using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineCameraManagerService : Singleton<CineCameraManagerService>
{
    public GameObject floorObj;
    CinemachineFreeLook cineCamera => GlobalContext.Instance.freeLookCamera;
    ZengaBlocksManagerService zengaBlocksManagerService => ZengaBlocksManagerService.Instance;

    void Start()
    {
        SwitchCameraLookat(floorObj);
        zengaBlocksManagerService.OnZengaBlocksRefreshed += OnZengaBlocksRefreshed;
    }

    void OnDestroy()
    {
        zengaBlocksManagerService.OnZengaBlocksRefreshed -= OnZengaBlocksRefreshed;
    }

    void OnZengaBlocksRefreshed()
    {
        SwitchCameraLookat(zengaBlocksManagerService.zengaParentBlocks[0]);
    }

    public void SwitchCameraLookat(GameObject target)
    {
        cineCamera.LookAt = target.transform;
        cineCamera.Follow = target.transform;
    }
}
