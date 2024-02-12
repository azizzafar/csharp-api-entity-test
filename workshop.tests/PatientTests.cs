using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace workshop.tests;

public class Tests
{



    [Test]
    public async Task GetAllAppointmentEndpointStatus()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/appointment");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task GetAppointmentWithId_2()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/appointment/2");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
    }


    [Test]
    public async Task GetAppointmentWithDoctorId_5()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/appointment/doctor5");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task GetAppointmentWithPatientId_5()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/appointment/patient5");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
    }



    [Test]
    public async Task PatientEndpointStatusDoctor()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/doctor");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
    }



    [Test]
    public async Task PatientEndpointStatusGetAll()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/doctor");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
    }


    [Test]
    public async Task PatientEndpointStatusNotFound()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/doctor/300");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }


    [Test]
    public async Task PatientEndpointStatusOk()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/doctor/300");

        // Assert
        Assert.IsFalse(response.StatusCode == System.Net.HttpStatusCode.OK);
    }



}