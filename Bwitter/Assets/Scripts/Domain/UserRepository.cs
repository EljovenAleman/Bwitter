﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserRepository
{
    Dictionary<string, string> users = new Dictionary<string, string>();

    public void Register(string name, string nickname)
    {
        users.Add(nickname, name);
    }

    public bool IsRegistered(string nickname)
    {
        return users.ContainsKey(nickname);                
    }
}