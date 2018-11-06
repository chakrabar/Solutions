using Wikipedia.Models.Index;

namespace Wikipedia.AppContracts
{
    public interface IContentIndexBuilder
    {
        ContentIndex Build(string paragraph);
    }
}
