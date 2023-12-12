using WpfApp.Model;

namespace WpfApp.Core.Repo
{
    public interface ITestRepo
    {
        long Insert(testModel model);
        void Update(testModel model);
        void Delete(int modelPK);
    }
}