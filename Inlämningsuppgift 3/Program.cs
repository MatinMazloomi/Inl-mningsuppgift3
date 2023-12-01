// Gränssnitt för databasoperationer
public interface IDatabase
{
    void AddUser(User user);
    void RemoveUser(int userId);
    User GetUser(int userId);
}

// Användarklass
public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
}

// UserManager-klass som använder IDatabase
public class UserManager
{
    private readonly IDatabase _database;

    public UserManager(IDatabase database)
    {
        _database = database;
    }

    public void AddUser(User user)
    {
        _database.AddUser(user);
    }

    public void RemoveUser(int userId)
    {
        _database.RemoveUser(userId);
    }

    public User GetUser(int userId)
    {
        return _database.GetUser(userId);
    }
}

// Nu kan du skriva tester med Moq
public class UserManagerTests
{
    [Fact]
    public void AddUser_ShouldCallAddUserMethod()
    {
        // Arrange
        var mockDatabase = new Mock<IDatabase>();
        var userManager = new UserManager(mockDatabase.Object);
        var user = new User { UserId = 1, UserName = "TestUser" };

        // Act
        userManager.AddUser(user);

        // Assert
        mockDatabase.Verify(d => d.AddUser(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public void RemoveUser_ShouldCallRemoveUserMethod()
    {
        // Arrange
        var mockDatabase = new Mock<IDatabase>();
        var userManager = new UserManager(mockDatabase.Object);
        var userId = 1;

        // Act
        userManager.RemoveUser(userId);

        // Assert
        mockDatabase.Verify(d => d.RemoveUser(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void GetUser_ShouldCallGetUserMethod()
    {
        // Arrange
        var mockDatabase = new Mock<IDatabase>();
        var userManager = new UserManager(mockDatabase.Object);
        var userId = 1;

        // Act
        var result = userManager.GetUser(userId);

        // Assert
        mockDatabase.Verify(d => d.GetUser(It.IsAny<int>()), Times.Once);
    }
}
