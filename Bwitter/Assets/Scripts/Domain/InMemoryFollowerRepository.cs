using System.Collections.Generic;

public class InMemoryFollowerRepository : IFollowerRepository
{
    Dictionary<string, List<string>> followersByNickname = new Dictionary<string, List<string>>();

    public void Follow(string followerNickname, string followeeNickname)
    {
        if (!followersByNickname.ContainsKey(followerNickname))
        {
            followersByNickname.Add(followerNickname, new List<string>());
        }
        followersByNickname[followerNickname].Add(followeeNickname);
    }

    public bool IsFollowing(string follower, string followee)
    {
        return followersByNickname[follower].Contains(followee);
    }


}
