using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceResponse<T>

{
    public T Data;
    public bool Success;
    public string Message;
}