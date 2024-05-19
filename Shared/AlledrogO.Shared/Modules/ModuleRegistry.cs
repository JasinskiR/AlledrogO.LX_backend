namespace AlledrogO.Shared.Modules;

internal class ModuleRegistry : IModuleRegistry
{
    private readonly Dictionary<string, ModuleRequestRegistration> _requestRegistrations = new();
    
    public ModuleRequestRegistration GetRequestRegistration(string path)
        => _requestRegistrations.TryGetValue(path, out var registration) ? registration : null;
    

    public void AddRequestAction(string path, ModuleRequestRegistration registration)
    {
        if (String.IsNullOrEmpty(path))
        {
            throw new InvalidOperationException("Request path cannot be null or empty");
        }

        if (registration.GetType().Namespace is null)
        {
            throw new InvalidOperationException("Request registration namespace cannot be null");
        }
        
        _requestRegistrations.Add(path, registration);
    }
}