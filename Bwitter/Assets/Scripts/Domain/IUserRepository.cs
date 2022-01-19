public interface IUserRepository
{
    string GetNameFromNickName(string nickname);
    bool IsRegistered(string nickname);
    void Register(string name, string nickname);
    void UpdateUserName(string newName, string nickname);
}