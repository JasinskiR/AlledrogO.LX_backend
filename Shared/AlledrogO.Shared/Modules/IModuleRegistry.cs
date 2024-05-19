namespace AlledrogO.Shared.Modules;

internal interface IModuleRegistry
{
    ModuleRequestRegistration GetRequestRegistration(string path);
    void AddRequestAction(string path, ModuleRequestRegistration registration);
}