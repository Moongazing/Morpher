Morpher Mapping Library

Morpher is a lightweight and customizable object-to-object mapping library designed to simplify data transformations in .NET applications. It supports advanced features such as:

    Custom Mapping: Define custom mapping rules between source and destination types using lambda expressions.
    Auto-Mapping: Automatically maps properties with matching names if no custom mapping is defined.
    Two-Way Mapping: Supports bidirectional mapping between objects.
    Deep Mapping: Handles nested object transformations.
    Profiles: Organize mapping configurations into reusable profiles.
    Error Logging: Logs detailed errors for debugging failed mappings.
    Performance Optimized: Uses caching for repeated mappings to improve performance.

How to Use

    Define Your Classes:

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
}

Create a Profile:

public class UserMappingProfile : BaseProfile
{
    public override void ConfigureMappings(MorpherManager manager)
    {
        manager.RegisterMapping<User, UserDto>(user => new UserDto
        {
            Id = user.Id,
            FullName = $"{user.FirstName} {user.LastName}"
        });
    }
}

Map Objects:

    var manager = new MorpherManager();
    var profile = new UserMappingProfile();
    profile.ConfigureMappings(manager);

    var user = new User { Id = 1, FirstName = "John", LastName = "Doe" };
    var userDto = manager.Map<User, UserDto>(user);

    Console.WriteLine($"Id: {userDto.Id}, FullName: {userDto.FullName}");

Key Features

    Simple API for registering and using mappings.
    Automatically maps properties with matching names.
    Modular and reusable configurations via profiles.
    Lightweight and easy to integrate.

Get started with Morpher today and simplify your data transformation needs!
