using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using Newtonsoft.Json;

public class JsonUserRepositoryShould
{
    JsonUserRepository jsonUserRepo;
    IUserPersistenceService persistenceService;

    [SetUp]
    public void SetUp()
    {
        persistenceService = Substitute.For<IUserPersistenceService>();
        jsonUserRepo = new JsonUserRepository(persistenceService);
    }

    [Test]
    public void Register_A_User_Using_Json_And_Save_Them()
    {
        //Given
        string name, nickname;
        name = "Diego";
        nickname = "Bettendorf";
        Dictionary<string, string> users = new Dictionary<string, string>();
        users.Add(nickname, name);

        string expectedSerialization = JsonConvert.SerializeObject(users);

        //When
        jsonUserRepo.Register(name, nickname);

        //Then
        persistenceService.Received(1).Save(expectedSerialization);
    }


}
