using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace SalesDataModel.Tests.Environments;

public class FakeEnvironment : IWebHostEnvironment
{
    [Obsolete]
    public FakeEnvironment(string envName) => EnvironmentName = envName;
    public string EnvironmentName { get; set; }
    public string ApplicationName { get; set; } = string.Empty;
    public string WebRootPath { get; set; } = string.Empty;
    public string ContentRootPath { get; set; } = string.Empty;
    public IFileProvider WebRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
