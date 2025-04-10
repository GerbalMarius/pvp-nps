namespace nps;

/// <summary>
/// Indicates that a method manages database transactions.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class Transactional : Attribute
{
    
}