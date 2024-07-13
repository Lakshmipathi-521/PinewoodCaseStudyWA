namespace ORM.CRMSystem;

public interface ISystemContext: IDisposable
{
    bool CommitChanges();
}
