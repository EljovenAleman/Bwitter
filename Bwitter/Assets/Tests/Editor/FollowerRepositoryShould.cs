using NUnit.Framework;

public class FollowerRepositoryShould
{
    [Test]
    public void Return_True_If_A_User_Is_Following_The_Other()
    {
        //Given        
        string UserNickname = "EljovenAleman";

        string UserNickname2 = "Perezoso";

        InMemoryFollowerRepository followerRepo = new InMemoryFollowerRepository();

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

        InMemoryFollowerRepository followerRepo = new InMemoryFollowerRepository();

        //When
        followerRepo.Follow(UserNickname, UserNickname2);
        followerRepo.Follow(UserNickname, UserNickname3);

        //Then
        Assert.IsTrue(followerRepo.IsFollowing(UserNickname, UserNickname2));
        Assert.IsTrue(followerRepo.IsFollowing(UserNickname, UserNickname3));
    }
}
