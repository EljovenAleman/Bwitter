﻿using System.Collections;
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
         
}
