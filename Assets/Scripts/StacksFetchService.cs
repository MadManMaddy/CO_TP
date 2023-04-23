using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class StacksFetchService : Singleton<StacksFetchService>
{
    // service to fetch json from api
    public void FetchStacks()
    {
        StartCoroutine(FetchStacksCoroutine());
    }

    private IEnumerator FetchStacksCoroutine()
    {
        // fetch json from api
        var www = new WWW(
            "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack"
        );
        yield return www;

        // parse json
        var json = www.text;
        var stacks = JsonConvert.DeserializeObject<List<Stack>>(json);
        StacksManagerService.Instance.RefreshStacks(stacks);
    }
}
