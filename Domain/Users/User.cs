namespace Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public bool HasPublicProfile { get; private set; }

    private User()
    {
        
    }

    private User(Guid id, string name, string email, bool hasPublicProfile)
    {
        Id = id;
        Name = name;
        Email = email;
        HasPublicProfile = hasPublicProfile;
    }

    public static User Create(
        string name,
        string email,
        bool hasPublicProfile)
    {
        return new(Guid.NewGuid(), name, email, hasPublicProfile);
    }
}
