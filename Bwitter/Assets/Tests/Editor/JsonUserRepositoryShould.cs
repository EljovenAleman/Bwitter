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
        persistenceService = new FakeUserPersistenceService();
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
        Assert.AreEqual(expectedSerialization, persistenceService.Load());
    }

    [Test]
    public void Register_Two_Users_Using_Jason_And_Save_Them()
    {
        //Given
        string name, nickname, name2, nickname2;
        name = "Diego";
        nickname = "Bettendorf";

        name2 = "Santi";
        nickname2 = "Perez";

        Dictionary<string, string> users = new Dictionary<string, string>();
        users.Add(nickname, name);
        users.Add(nickname2, name2);

        string expectedSerialization = JsonConvert.SerializeObject(users);        

        //When
        jsonUserRepo.Register(name,nickname);              
        jsonUserRepo.Register(name2, nickname2);

        //Then
        Assert.AreEqual(expectedSerialization, persistenceService.Load());
    }

    [Test]
    public void Return_A_Name_Using_A_NickName()
    {
        //Given
        string name, nickname, name2, nickname2;
        name = "Diego";
        nickname = "Bettendorf";

        name2 = "Santi";
        nickname2 = "Perez";

        Dictionary<string, string> users = new Dictionary<string, string>();
        users.Add(nickname, name);
        users.Add(nickname2, name2);

        string serialization = JsonConvert.SerializeObject(users);
        persistenceService.Save(serialization);

        //When
        var returnedName = jsonUserRepo.GetNameFromNickName(nickname);
        var returnedName2 = jsonUserRepo.GetNameFromNickName(nickname2);

        //Then
        Assert.AreEqual(name, returnedName);
        Assert.AreEqual(name2, returnedName2);
    }


}

public class FakeUserPersistenceService : IUserPersistenceService
{
    string serializedUsers = "{}";

    public string Load()
    {
        return serializedUsers;
    }

    public void Save(string serializedUsers)
    {
        this.serializedUsers = serializedUsers;
    }
}
