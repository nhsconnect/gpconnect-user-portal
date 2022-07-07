namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;

public interface ITempDataProviderService
{
    T GetItem<T>(string key) where T : class;
    void PutItem<T>(string key, T value) where T : class;
    void RemoveItem(string key);
    void RemoveAll();
    bool HasItems { get; }
}
