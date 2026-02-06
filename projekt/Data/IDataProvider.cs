using SystemRezerwacjiSal.Models;

namespace SystemRezerwacjiSal.Data;

public interface IDataProvider
{
    List<Zajecia> Load();
    void Insert(Zajecia z);
    void Delete(int zajeciaId);
}
