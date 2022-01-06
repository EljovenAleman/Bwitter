using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserRepository
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

public class FollowerRepository
{
    Dictionary<string, List<string>> followers = new Dictionary<string, List<string>>();

    public void Follow(string follower, string followee)
    {        
        if(!followers.ContainsKey(follower))
        {
            followers.Add(follower, new List<string>());
        }
        followers[follower].Add(followee);                        
    }

    public bool IsFollowing(string follower, string followee)
    {
        return followers[follower].Contains(followee);
    }


}
