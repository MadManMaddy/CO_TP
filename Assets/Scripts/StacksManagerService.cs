using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class StacksManagerService : Singleton<StacksManagerService>
{
    public Dictionary<string, List<Stack>> gradeStacks = new Dictionary<string, List<Stack>>();

    private void Start()
    {
        StacksFetchService.Instance.FetchStacks();
    }

    public void RefreshStacks(List<Stack> stacks)
    {
        gradeStacks.Clear();
        foreach (var stack in stacks)
        {
            if (!gradeStacks.ContainsKey(stack.grade))
            {
                gradeStacks.Add(stack.grade, new List<Stack>());
            }
            gradeStacks[stack.grade].Add(stack);
        }
        // LogStacks();
        ZengaBlocksManagerService.Instance.RefreshZengaBlocks();
    }

    void LogStacks()
    {
        foreach (var grade in gradeStacks.Keys)
        {
            Debug.Log(grade);
            foreach (var stack in gradeStacks[grade])
            {
                Debug.Log(JsonConvert.SerializeObject(stack));
            }
        }
    }
}
