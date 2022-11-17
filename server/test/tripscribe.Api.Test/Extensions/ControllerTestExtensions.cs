using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace tripscribe.Api.Test.Extensions;

[ExcludeFromCodeCoverage]
public static class ControllerTestExtensions
{
    public static T AssertObjectResult<T, TU>(this ActionResult<T> @this) where TU : ActionResult
    {
        @this.Result.Should().BeOfType<TU>();
        return (T)((ObjectResult)@this.Result).Value;
    }
        
    public static void AssertResult<T, TU>(this ActionResult<T> @this) where TU : ActionResult
    {
        @this.Result.Should().BeOfType<TU>();
        @this.Result.Should().NotBeAssignableTo<ObjectResult>();
    }

    public static void AssertResult<T>(this ActionResult @this) 
    {
        @this.Should().BeOfType<T>();
        @this.Should().NotBeAssignableTo<ObjectResult>();
    }
}