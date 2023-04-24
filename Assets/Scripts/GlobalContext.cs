using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GlobalContext : Singleton<GlobalContext>
{
    public GameObject zengaBlockPrefab;
    public GameObject gradeNameTextPrefab;

    [Header("UI references")]
    public GameObject gradeButtonPrefab;
    public Transform gradeButtonParent;
    public TMP_Text stackLogText;
    public GameObject stackLogPanel;
    public GameObject refreshStacksButton;
    public GameObject removeGlassStacksButton;

    public CinemachineFreeLook freeLookCamera;

    [SerializeField]
    List<ZengaMaterials> zengaMaterials = new List<ZengaMaterials>();
    public List<ZengaDefaultPosition> zengaBlockPositions = new List<ZengaDefaultPosition>()
    {
        new ZengaDefaultPosition()
        {
            position = new Vector3(-2.5f, 0, 0),
            rotation = new Vector3(0, 0, 0)
        },
        new ZengaDefaultPosition()
        {
            position = new Vector3(0, 0, 0),
            rotation = new Vector3(0, 0, 0)
        },
        new ZengaDefaultPosition()
        {
            position = new Vector3(2.5f, 0, 0),
            rotation = new Vector3(0, 0, 0)
        },
        new ZengaDefaultPosition()
        {
            position = new Vector3(0, 0, -2.5f),
            rotation = new Vector3(0, 90, 0)
        },
        new ZengaDefaultPosition()
        {
            position = new Vector3(0, 0, 0),
            rotation = new Vector3(0, 90, 0)
        },
        new ZengaDefaultPosition()
        {
            position = new Vector3(0, 0, 2.5f),
            rotation = new Vector3(0, 90, 0)
        },
    };

    public Material GetMaterialByStackMastery(int masteryLevel)
    {
        return zengaMaterials[masteryLevel].material;
    }

    //Test code for spawning st
    // private void Start()
    // {
    //     for (int i = 0; i < 30; i++)
    //     {
    //         var height = i / 3;
    //         var rotationalPosition = i % 6;
    //         var materialSelection = i % 3;

    //         ZengaDefaultPosition currentPos = zengaBlockPositions[rotationalPosition];

    //         // original correct position is height * 1.5f, added 0.2f offset to height to make it look cooler
    //         // currentPos.position.y = height * 1.5f;
    //         currentPos.position.y = height * 2f + 0.2f * materialSelection;
    //         var zengaBlock = Instantiate(
    //             zengaBlockPrefab,
    //             currentPos.position,
    //             Quaternion.Euler(currentPos.rotation)
    //         );

    //         zengaBlock.GetComponent<Renderer>().material = zengaMaterials[
    //             materialSelection
    //         ].material;
    //     }
    // }
}

[System.Serializable]
public class ZengaMaterials
{
    public ZengaMaterialTypes type;
    public Material material;
}

[System.Serializable]
public class ZengaDefaultPosition
{
    public Vector3 position;
    public Vector3 rotation;

    public void Duplicate(ZengaDefaultPosition zdp)
    {
        position = new Vector3(zdp.position.x, zdp.position.y, zdp.position.z);
        rotation = new Vector3(zdp.rotation.x, zdp.rotation.y, zdp.rotation.z);
    }
}

public enum ZengaMaterialTypes
{
    Glass = 0,
    Wood = 1,
    Stone = 2,
}
