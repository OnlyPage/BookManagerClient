using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResponseClass 
{
    public string timestamp;
    public int status;
    public string error;
    public string trace;
    public string message;
    public string path;
}
