using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Newtonsoft.Json;
using NSubstitute;

public class JsonFollowerRepositoryShould : MonoBehaviour
{
    IFollowerRepository followerRepo;
    IFollowerPersistenceService persistenceService;
    IUserRepository userRepo;

    [SetUp]
    public void SetUp()
    {
        persistenceService = new FakeFollowerPersistenceService();
        userRepo = Substitute.For<IUserRepository>();
        followerRepo = new JsonFollowerRepository(persistenceService, userRepo);        
    }

    [Test]
    public void Follow_Another_User_Using_The_Nickname()
    {
        //Given        
        string followerNickname = "EljovenAleman";
        string followeeNickname = "Perezoso";

        List<string> followeesList = new List<string>();
        followeesList.Add(followeeNickname);

        Dictionary<string, List<string>> followersByNickname = new Dictionary<string, List<string>>();
        followersByNickname.Add(followerNickname,followeesList);

        string expectedSerialization = JsonConvert.SerializeObject(followersByNickname);

        userRepo.IsRegistered(followerNickname).Returns(true);
        userRepo.IsRegistered(followeeNickname).Returns(true);

        //When
        followerRepo.Follow(followerNickname, followeeNickname);

        //Then
        Assert.AreEqual(expectedSerialization, persistenceService.Load());
       
    }

    [Test]
    public void Return_True_If_A_User_Is_Following_Another_Using_The_Nickname()
    {
        //Given
        string followerNickname = "EljovenAleman";

        string followeeNickname = "Samte";
        string followeeNickname2 = "Kenny";


        userRepo.IsRegistered(followerNickname).Returns(true);
        userRepo.IsRegistered(followeeNickname).Returns(true);
        userRepo.IsRegistered(followeeNickname2).Returns(true);

        followerRepo.Follow(followerNickname, followeeNickname);        

        //WhenThen
        Assert.IsTrue(followerRepo.IsFollowing(followerNickname, followeeNickname));
        Assert.IsFalse(followerRepo.IsFollowing(followerNickname, followeeNickname2));
    }        

    [Test]
    public void Throw_An_User_Doesnt_Exist_Exception_When_Is_Following_Is_Called_With_An_Invalid_Nickname()
    {
        //Given
        string follower = "EljovenAleman";
        string followee = "Samte";

        userRepo.IsRegistered(follower).Returns(false);
        userRepo.IsRegistered(followee).Returns(true);

        //WhenThen

        Assert.Throws<UserDoesntExistException>(() => followerRepo.IsFollowing(follower, followee));

        //And

        //Given
        userRepo.IsRegistered(follower).Returns(true);
        userRepo.IsRegistered(followee).Returns(false);

        //WhenThen
        Assert.Throws<UserDoesntExistException>(() => followerRepo.IsFollowing(follower, followee));
    }

    [Test]
    public void Throw_An_User_Doesnt_Exist_Exception_When_Follow_Is_Called_With_An_Invalid_Nickname()
    {
        //Given
        string follower = "EljovenAleman";
        string followee = "Samte";

        userRepo.IsRegistered(follower).Returns(false);
        userRepo.IsRegistered(followee).Returns(true);

        //WhenThen

        Assert.Throws<UserDoesntExistException>(() => followerRepo.Follow(follower, followee));

        //And

        //Given
        userRepo.IsRegistered(follower).Returns(true);
        userRepo.IsRegistered(followee).Returns(false);

        //WhenThen
        Assert.Throws<UserDoesntExistException>(() => followerRepo.Follow(follower, followee));
    }

    [Test]
    public void Return_False_When_Is_Registered_Is_Called_With_Valid_Nicknames_That_Are_Not_Following_Anyone()
    {
        //Given
        string follower = "EljovenAleman";
        string followee = "Samte";

        userRepo.IsRegistered(follower).Returns(true);
        userRepo.IsRegistered(followee).Returns(true);

        //WhenThen
        Assert.IsFalse(followerRepo.IsFollowing(follower, followee));
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
