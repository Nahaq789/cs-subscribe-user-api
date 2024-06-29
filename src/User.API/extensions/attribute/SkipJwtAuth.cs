namespace User.API.application.extensions.attribute;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class SkipJwtAuth : Attribute
{
}