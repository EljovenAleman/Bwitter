using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;

public class Test
{
    [Test]
    public void Register_A_User_With_A_Name_And_A_Nickname()
    {
        //Given
        string name = "Diego";
        string nickname = "EljovenAleman";
        UserRepository userRepository = new UserRepository();

        //When

        userRepository.Register(name, nickname);

        //Then
        Assert.IsTrue(userRepository.IsRegistered(nickname));
        

    }

    [Test]
    public void Return_False_When_IsRegistered_Is_Called_And_No_User_Is_Registered()
    {
        //Given
        UserRepository userRepository = new UserRepository();

        //WhenThen
        Assert.IsFalse(userRepository.IsRegistered("diego"));
    }

    [Test]
    public void Return_False_When_IsRegistered_Is_Called_With_A_Non_Registered_User()
    {
        //Given
        string registeredUserName = "Diego";
        string registeredUserNickname = "EljovenAleman";
        string nonRegisteredUser = "Samte";
        UserRepository userRepository = new UserRepository();
        userRepository.Register(registeredUserName, registeredUserNickname);

        //WhenThen
        Assert.IsFalse(userRepository.IsRegistered(nonRegisteredUser));
    }

    [Test]
    public void Throw_An_Error_When_Register_Is_Called_With_A_Nickname_That_Is_Already_Registered()
    {
        //Given
        string UserName = "Diego";
        string UserNickname = "EljovenAleman";

        string UserName2 = "Santi";
        UserRepository userRepository = new UserRepository();

        userRepository.Register(UserName, UserNickname);

        //When
        Assert.Throws<UserAlreadyRegisteredException>(() => userRepository.Register(UserName2, UserNickname));
    }

    [Test]
    public void Let_The_User_Update_Their_Real_Name()
    {
        //given
        string UserName = "Diego";
        string UserNickname = "EljovenAleman";

        string newName = "Marado";

        UserRepository userRepository = new UserRepository();

        userRepository.Register(UserName, UserNickname);

        //When
        userRepository.UpdateUserName(newName, UserNickname);

        //Then
        Assert.AreEqual(newName, userRepository.GetNameFromNickName(UserNickname));
    }

    [Test]
    public void Return_True_If_A_User_Is_Following_The_Other()
    {
        //Given        
        string UserNickname = "EljovenAleman";
        
        string UserNickname2 = "Perezoso";
        
        FollowerRepository followerRepo = new FollowerRepository();

        //When
        followerRepo.Follow(UserNickname, UserNickname2);

        //Then
        Assert.IsTrue(followerRepo.IsFollowing(UserNickname, UserNickname2));

    }

    [Test]
    public void Return_True_For_Each_Followee_Followed()
    {
        //Given
        string UserNickname = "EljovenAleman";

        string UserNickname2 = "Perezoso";

        string UserNickname3 = "Kenny4";
        
        FollowerRepository followerRepo = new FollowerRepository();

        //When
        followerRepo.Follow(UserNickname, UserNickname2);
        followerRepo.Follow(UserNickname, UserNickname3);

        //Then
        Assert.IsTrue(followerRepo.IsFollowing(UserNickname, UserNickname2));
        Assert.IsTrue(followerRepo.IsFollowing(UserNickname, UserNickname3));
    }
         
}
