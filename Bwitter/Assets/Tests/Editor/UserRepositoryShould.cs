using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;

public class UserRepositoryShould
{
    [Test]
    public void Register_A_User_With_A_Name_And_A_Nickname()
    {
        //Given
        string name = "Diego";
        string nickname = "EljovenAleman";
        InMemoryUserRepository userRepository = new InMemoryUserRepository();

        //When

        userRepository.Register(name, nickname);

        //Then
        Assert.IsTrue(userRepository.IsRegistered(nickname));
        

    }

    [Test]
    public void Return_False_When_IsRegistered_Is_Called_And_No_User_Is_Registered()
    {
        //Given
        InMemoryUserRepository userRepository = new InMemoryUserRepository();

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
        InMemoryUserRepository userRepository = new InMemoryUserRepository();
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
        InMemoryUserRepository userRepository = new InMemoryUserRepository();

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

        InMemoryUserRepository userRepository = new InMemoryUserRepository();

        userRepository.Register(UserName, UserNickname);

        //When
        userRepository.UpdateUserName(newName, UserNickname);

        //Then
        Assert.AreEqual(newName, userRepository.GetNameFromNickName(UserNickname));
    }

                     
}
