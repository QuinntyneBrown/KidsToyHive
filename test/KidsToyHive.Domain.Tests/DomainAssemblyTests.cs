using FluentAssertions;
using NUnit.Framework;

namespace KidsToyHive.Domain.Tests;

[TestFixture]
public class DomainAssemblyTests
{
    [Test]
    public void DomainAssembly_ShouldExist()
    {
        // Arrange & Act
        var assembly = typeof(KidsToyHive.Domain.KidsToyHive_Domain).Assembly;

        // Assert
        assembly.Should().NotBeNull();
        assembly.GetName().Name.Should().Be("KidsToyHive.Domain");
    }

    [Test]
    public void DomainAssembly_ShouldReferenceCore()
    {
        // Arrange
        var assembly = typeof(KidsToyHive.Domain.KidsToyHive_Domain).Assembly;

        // Act
        var referencedAssemblies = assembly.GetReferencedAssemblies();

        // Assert
        referencedAssemblies.Should().Contain(a => a.Name == "KidsToyHive.Core");
    }

    [Test]
    public void DomainAssembly_ShouldTargetNet10()
    {
        // Arrange
        var assembly = typeof(KidsToyHive.Domain.KidsToyHive_Domain).Assembly;

        // Act
        var targetFramework = assembly.GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), false)
            .Cast<System.Runtime.Versioning.TargetFrameworkAttribute>()
            .FirstOrDefault();

        // Assert
        targetFramework.Should().NotBeNull();
        targetFramework!.FrameworkName.Should().Contain("Version=v10.0");
    }
}
