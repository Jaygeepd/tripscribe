﻿using Newtonsoft.Json.Schema;

namespace Tripscribe.Api.Integration.Test.TestUtilities;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NJsonSchema.Generation;

public static class JsonValidator
{
    public static string VerifyDeSerialization<T>(this string @this)
    {
        var (dataToValidate, schema) = RetrieveSchemaForComparison<T>(@this);
        var errors = schema.Validate(dataToValidate);
        errors.Should().BeEmpty();

        return @this;
    }

    private static (string, JsonSchema) RetrieveSchemaForComparison<T>(string data)
    {
        var type = typeof(T);
        if (type.IsArray)
        {
            var elementSchema = JsonSchema.FromType(type.GetElementType()!, RetrieveJsonSettings());
            var elementObject = JArray.Parse(data).ElementAt(0).ToString();
            return (elementObject, elementSchema);
        }

        return (data, JsonSchema.FromType(type, RetrieveJsonSettings()));
    }

    private static JsonSchemaGeneratorSettings RetrieveJsonSettings()
    {
        return new()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        };
    }
}