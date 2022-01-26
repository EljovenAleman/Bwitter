public interface IFollowerRepository
{
    void Follow(string followerNickname, string followeeNickname);
    bool IsFollowing(string follower, string followee);
}
