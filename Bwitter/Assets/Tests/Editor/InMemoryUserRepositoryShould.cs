using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;

public class InMemoryUserRepositoryShould
{
    InMemoryUserRepository userRepository;

    [SetUp]
    public void SetUp()
    {
        userRepository = new InMemoryUserRepository();
    }

    [Test]
    public void Register_A_User_With_A_Name_And_A_Nickname()
    {
        //Given
        string name = "Diego";
        string nickname = "EljovenAleman";        

        //When

        userRepository.Register(name, nickname);

        //Then
        Assert.IsTrue(userRepository.IsRegistered(nickname));
        

    }

    [Test]
    public void Return_False_When_IsRegistered_Is_Called_And_No_User_Is_Registered()
    {                
        //GivenWhenThen
        Assert.IsFalse(userRepository.IsRegistered("diego"));
    }

    [Test]
    public void Return_False_When_IsRegistered_Is_Called_With_A_Non_Registered_User()
    {
        //Given
        string registeredUserName = "Diego";
        string registeredUserNickname = "EljovenAleman";
        string nonRegisteredUser = "Samte";        
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

        userRepository.Register(UserName, UserNickname);

        //When
        userRepository.UpdateUserName(newName, UserNickname);

        //Then
        Assert.AreEqual(newName, userRepository.GetNameFromNickName(UserNickname));
    }

    [Test]
    public void Throw_An_Exception_When_Asked_To_Update_A_Name_For_A_Username_That_Doesnt_Exist()
    {
        //Given
        string nickname = "Aleman";        

        //WhenThen
        Assert.Throws<UserDoesntExistException>(() => userRepository.UpdateUserName("Marado", nickname));
    }


}
