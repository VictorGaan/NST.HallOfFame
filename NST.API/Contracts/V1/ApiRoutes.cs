namespace NST.API.Contracts.V1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = $"{Root}/{Version}";

        public static class Person
        {
            public const string GetAll = Base + "/persons";
            public const string Get = Base + "/person/{id}";
            public const string Update = Base + "/person/{id}";
            public const string Delete = Base + "/person/{id}";
            public const string Create = Base + "/person";
        }
    }
}
