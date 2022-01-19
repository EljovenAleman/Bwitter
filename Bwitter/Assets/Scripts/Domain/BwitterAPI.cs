using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BwitterAPI
{
    IUserRepository userRepository = new InMemoryUserRepository();

    IFollowerRepository followerRepository = new InMemoryFollowerRepository();

    //UserRepository
    public void Register(string name, string nickname)
    {
        userRepository.Register(name, nickname);
    }
    public bool IsRegistered(string nickname)
    {
        return userRepository.IsRegistered(nickname);
    }

    public string GetNameFromNickName(string nickname)
    {
        return userRepository.GetNameFromNickName(nickname);
    }

    public void UpdateUserName(string newName, string nickname)
    {
        userRepository.UpdateUserName(newName, nickname);
    }




    //FollowerRepository
    public void Follow(string followerNickname, string followeeNickname)
    {
        followerRepository.Follow(followerNickname, followeeNickname);
    }

    public bool IsFollowing(string follower, string followee)
    {
        return followerRepository.IsFollowing(follower, followee);
    }
}


