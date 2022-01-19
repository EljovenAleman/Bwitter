using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMemoryUserRepository : IUserRepository
{
    Dictionary<string, string> users = new Dictionary<string, string>();

    public void Register(string name, string nickname)
    {
        try
        {
            users.Add(nickname, name);
        }
        catch
        {
            throw new UserAlreadyRegisteredException();
        }

    }

    public bool IsRegistered(string nickname)
    {
        return users.ContainsKey(nickname);
    }

    public void UpdateUserName(string newName, string nickname)
    {
        users[nickname] = newName;
    }

    public string GetNameFromNickName(string nickname)
    {
        return users[nickname];
    }
}


