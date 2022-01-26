using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Newtonsoft.Json;

public class JsonFollowerRepositoryShould : MonoBehaviour
{
    IFollowerRepository followerRepo;
    IFollowerPersistenceService persistenceService;

    [SetUp]
    public void SetUp()
    {
        persistenceService = new FakeFollowerPersistenceService();
        followerRepo = new JsonFollowerRepository(persistenceService);
    }

    [Test]
    public void Follow_Another_User_Using_The_Nickname()
    {
        //Given        
        string followerNickname = "EljovenAleman";

        List<string> followeesList = new List<string>();
        string followeeNickname = "Perezoso";
        followeesList.Add(followeeNickname);

        Dictionary<string, List<string>> followersByNickname = new Dictionary<string, List<string>>();
        followersByNickname.Add(followerNickname,followeesList);

        string expectedSerialization = JsonConvert.SerializeObject(followersByNickname);

        //When
        followerRepo.Follow(followerNickname, followeeNickname);

        //Then
        Assert.AreEqual(expectedSerialization, persistenceService.Load());
       
    }

    

}

public class FakeFollowerPersistenceService : IFollowerPersistenceService
{    
    private string serializedString;

    public string Load()
    {
        return serializedString;
    }

    public void Save(string serializedObject)
    {
        serializedString = serializedObject;
    }
}
