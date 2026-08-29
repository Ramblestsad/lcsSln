using System.ComponentModel.DataAnnotations;

namespace Todo.Orders.Api;

internal static class RequestValidation
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
        if (Validator.TryValidateObject(request, new ValidationContext(request), results, true))
        {
            return true;
        }

        foreach (var result in results)
        {
            foreach (var member in result.MemberNames.DefaultIfEmpty("request"))
            {
                var message = result.ErrorMessage ?? "The request is invalid.";
                errors[member] = errors.TryGetValue(member, out var messages)
                    ? [.. messages, message]
                    : [message];
            }
        }

        return false;
    }
}
