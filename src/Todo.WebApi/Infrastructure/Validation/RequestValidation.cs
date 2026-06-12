using System.ComponentModel.DataAnnotations;

namespace Todo.WebApi.Infrastructure.Validation;

public static class RequestValidation
{
    public static bool TryValidate<T>(T? request, out Dictionary<string, string[]> errors)
    {
        errors = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
        if (request is null)
        {
            errors["request"] = ["Request body is required."];
            return false;
        }

        var results = new List<ValidationResult>();
        var context = new ValidationContext(request);
        if (Validator.TryValidateObject(request, context, results, validateAllProperties: true))
        {
            return true;
        }

        foreach (var result in results)
        {
            var members = result.MemberNames.Any()
                ? result.MemberNames
                : [string.Empty];
            foreach (var member in members)
            {
                var key = string.IsNullOrWhiteSpace(member) ? "request" : member;
                if (!errors.TryGetValue(key, out var messages))
                {
                    errors[key] = [result.ErrorMessage ?? "The request is invalid."];
                    continue;
                }

                errors[key] = messages.Append(result.ErrorMessage ?? "The request is invalid.").ToArray();
            }
        }

        return false;
    }
}
