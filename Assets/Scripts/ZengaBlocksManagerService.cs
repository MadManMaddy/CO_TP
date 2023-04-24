using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;

public class ZengaBlocksManagerService : Singleton<ZengaBlocksManagerService>
{
    public Action OnZengaBlocksRefreshed;
    public List<GameObject> zengaParentBlocks = new List<GameObject>();
    public Vector3 offset = new Vector3(10, 0, 0);
    GlobalContext globalContext => GlobalContext.Instance;

    public Dictionary<string, List<ZengaBlock>> gradeZengaBlocks =
        new Dictionary<string, List<ZengaBlock>>();

    public string GetGradeName(int id)
    {
        return gradeZengaBlocks.Keys.ToList()[id];
    }

    public void RefreshZengaBlocks()
    {
        gradeZengaBlocks.Clear();
        zengaParentBlocks.ForEach(x => Destroy(x));
        zengaParentBlocks.Clear();

        var stacks = StacksManagerService.Instance.gradeStacks;

        int gradesCount = 0;

        stacks
            .ToList()
            .ForEach(grade =>
            {
                gradeZengaBlocks.Add(grade.Key, new List<ZengaBlock>());

                var gradeNameBlockPos = new Vector3(offset.x * gradesCount, offset.y, offset.z);
                var gradeNameBlock = Instantiate(
                    globalContext.gradeNameTextPrefab,
                    gradeNameBlockPos,
                    Quaternion.identity
                );
                gradeNameBlock.GetComponentInChildren<TMP_Text>().text = grade.Key;
                zengaParentBlocks.Add(gradeNameBlock);

                var orderedStacks = grade.Value
                    .OrderBy(x => x.domain)
                    .ThenBy(y => y.cluster)
                    .ThenBy(z => z.standardid)
                    .ToArray();

                for (int i = 0; i < orderedStacks.Length; i++)
                {
                    var height = i / 3;
                    var rotationalPosition = i % 6;
                    var materialSelection = i % 3;
                    ZengaDefaultPosition currentPos = new ZengaDefaultPosition();
                    currentPos.Duplicate(globalContext.zengaBlockPositions[rotationalPosition]);

                    currentPos.position.y = height * 2f + 0.2f * materialSelection;
                    currentPos.position.x += offset.x * gradesCount;

                    var zengaBlock = Instantiate(
                        globalContext.zengaBlockPrefab,
                        currentPos.position,
                        Quaternion.Euler(currentPos.rotation)
                    );
                    zengaBlock.transform.SetParent(gradeNameBlock.transform);
                    var zb = zengaBlock.AddComponent<ZengaBlock>();
                    zb.InitializeZengaBlock(orderedStacks[i], i);
                    gradeZengaBlocks[grade.Key].Add(zb);
                }
                gradesCount++;
            });
        if (gradeZengaBlocks.Count > 0 && gradeZengaBlocks.First().Value.Count > 0)
        {
            OnZengaBlocksRefreshed?.Invoke();
        }
    }

    public void RemoveGlassStacks()
    {
        gradeZengaBlocks
            .ToList()
            .ForEach(grade =>
            {
                grade.Value
                    .ToList()
                    .ForEach(stack =>
                    {
                        if (stack.stack.mastery == 0)
                        {
                            grade.Value.Remove(stack);
                            Destroy(stack.gameObject);
                        }
                    });
            });
    }
}
