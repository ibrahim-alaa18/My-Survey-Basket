namespace SurveyBasket.Abstractions.Consts;

public static class Permissions
{
    public const string Type = "permissions";

    // Polls
    public const string GetPolls = "polls:read";
    public const string AddPolls = "polls:add";
    public const string UpdatePolls = "polls:update";
    public const string DeletePolls = "polls:delete";

    // Questions
    public const string GetQuestions = "questions:read";
    public const string AddQuestions = "questions:add";
    public const string UpdateQuestions = "questions:update";

    // Users
    public const string GetUsers = "users:read";
    public const string AddUsers = "users:add";
    public const string UpdateUsers = "users:update";

    // Roles
    public const string GetRoles = "roles:read";
    public const string AddRoles = "roles:add";
    public const string UpdateRoles = "roles:update";

    // Results
    public const string Results = "results:read";

    
    public static readonly IReadOnlyList<string> All = new List<string>
    {
        GetPolls,
        AddPolls,
        UpdatePolls,
        DeletePolls,

        GetQuestions,
        AddQuestions,
        UpdateQuestions,

        GetUsers,
        AddUsers,
        UpdateUsers,

        GetRoles,
        AddRoles,
        UpdateRoles,

        Results
    }.AsReadOnly();
}
