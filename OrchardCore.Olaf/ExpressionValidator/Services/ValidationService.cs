using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using OrchardCore.Olaf.Common;
using OrchardCore.Olaf.Delay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;

namespace OrchardCore.Olaf.ExpressionValidator.Services
{
    public static class FieldTypes
    {
        public const  string StringField = "System.String";
        public const string BooleanField = "System.Boolean";
        public const string DateTimeField = "System.DateTime";
        public const string IntegerField = "System.Int32";

        public static IEnumerable<SelectListItem> BuildInTypes => new List<SelectListItem>
        {
            new SelectListItem("String",    StringField),
            new SelectListItem("Boolean", BooleanField),
            new SelectListItem("Integer", IntegerField),
            new SelectListItem("Date Time", DateTimeField),
        };
    }

    
    public class ValidationService
    {

        public async Task<bool> ValidateAsync(string propertyType, string propertyValue, string expression)
        {
            // Creates Type from fieldType
            Type type = System.Type.GetType(propertyType);

            // Creates scripting options for Roslyn API.
            ScriptOptions scriptingOptions = ScriptOptions.Default.WithImports("System");
            ScriptOptions.Default.AddReferences(System.Type.GetType(propertyType).Assembly);

            bool isValid = false;
            switch (propertyType)
            {
                case FieldTypes.DateTimeField:
                    var dateTest = await EvaluateAsync<DateTimeOffset>(expression, scriptingOptions);
                    DateTimeOffset target = DateTimeUtils.ParseDateString(propertyValue);
                    isValid = dateTest.Invoke(target);
                    break;
                case FieldTypes.BooleanField:
                    var boolTest = await EvaluateAsync<bool>(expression, scriptingOptions);
                    isValid = boolTest.Invoke(bool.Parse(propertyValue));
                    break;
                case FieldTypes.IntegerField:
                    var integerTest = await EvaluateAsync<Int32>(expression, scriptingOptions);
                    isValid = integerTest.Invoke(int.Parse(propertyValue));
                    break;
                case FieldTypes.StringField:
                    var stringTest = await EvaluateAsync<string>(expression, scriptingOptions);
                    isValid = stringTest.Invoke(propertyValue);
                    break;


            }


            return (bool?)isValid ?? false;
        }

        public async Task<Func<T, bool>> EvaluateAsync<T>(string expressionString, ScriptOptions options)
        {
            return await CSharpScript.EvaluateAsync<Func<T, bool>>(expressionString, options);
        }
    }
}
