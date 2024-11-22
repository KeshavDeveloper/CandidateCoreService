Setup and Run .NET Core Project with EF Migrations and NUnit Testing

1. Setup Database Connection for EF Core
Add Connection String: Ensure your appsettings.json contains the connection string:

{
    "ConnectionStrings": {
        "DefaultConnection": "Pooling=false;Data Source=.;Initial Catalog=Candidate;Trusted_Connection=True;"
    }
}
Ensure Proper Configuration: In Program.cs (or Startup.cs), configure the DbContext to use the connection string:
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        var app = builder.Build();
        app.Run();
    }
}

2. Set Up Entity Framework Core Migrations
Add Migrations: Open Package Manager Console or Terminal and run:

NOTE: If the migration file is already created, you can skip this step and directly run (dotnet ef database update). Alternatively, you can remove the migration folder and run both commands to recreate the database schema.

=>dotnet ef migrations add InitialCreate
Apply Migrations: After the migration is created, apply it to the database:

=> dotnet ef database update
..............................................................................................................................................................
3. Configure SSL and Connection Issues
SSL Trust Error: If you're facing an SSL trust error, add TrustServerCertificate=true to your connection string:

"DefaultConnection": "Data Source=.;Initial Catalog=Candidate;Integrated Security=True;TrustServerCertificate=True;"

Authentication Failure: For login failures, ensure proper user credentials are set in your connection string or configuration.


4. Testing with NUnit
Install NUnit: Ensure that NUnit and NUnit3TestAdapter are installed via NuGet:

Install-Package NUnit
Install-Package NUnit3TestAdapter

Create Test Class: Update the test class to use NUnit's [TestFixture], [SetUp], and [Test] attributes, and use NUnit's Assert methods:

[TestFixture]
public class CandidatesControllerTests
{
    private Mock<ICandidateRepository> _repositoryMock;
    private CandidatesController _controller;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<ICandidateRepository>();
        _controller = new CandidatesController(_repositoryMock.Object);
    }

    [Test]
    public async Task AddOrUpdateCandidate_ShouldCreate_WhenNew()
    {
        var dto = new CandidateDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Comment = "New Candidate"
        };

        _repositoryMock.Setup(repo => repo.GetByEmailAsync(dto.Email)).ReturnsAsync((Candidate)null);

        var result = await _controller.AddOrUpdateCandidate(dto);

        Assert.IsInstanceOf<CreatedAtActionResult>(result);
    }
}


5. Running the Application and Tests

Run the Application: Start the application in Visual Studio or via terminal:

dotnet run

Run the Tests: To run the tests, use Test Explorer in Visual Studio, or run the following command in the terminal:

dotnet test
This will execute all NUnit tests in your project.


6. Important Configuration Settings:
Database Connection: Ensure the DefaultConnection string is correctly configured for your environment (local or production).
SSL/Trust Certificate: If facing trust issues, use TrustServerCertificate=true in your connection string.
NUnit Setup: Use [TestFixture] for the test class, [SetUp] for setup before each test, and [Test] for individual test methods.
7. Common Errors:
Connection String Issues: Ensure the connection string is correct and the database server is accessible.
Login Failures: Double-check your SQL Server authentication mode and credentials in the connection string.
SSL Trust Error: Add TrustServerCertificate=true to your connection string if needed.
EF Migration Errors: If migrations are failing, ensure dotnet ef tools are installed and you're running the commands in the correct project directory.
