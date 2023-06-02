using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestStatic
{
    public static void Test1()
    {
        Debug.Log("Test 1 invoked");
    }

    public static void Test2()
    {
        Debug.Log("Test 2 invoked");
    }
}

public class Test1
{
    public void Test3()
    {
        Debug.Log("Test 3 invoked");
    }

    public void Test4()
    {
        Debug.Log("Test 4 invoked");
    }
}

public class Test2
{
    public void Test5()
    {
        Debug.Log("Test 5 invoked");
    }
}
