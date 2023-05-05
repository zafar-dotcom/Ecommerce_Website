using OnLineShop.Models;

namespace OnLineShop.IServices
{
    public interface ISpecialTag
    {
        List<SpecialTag_model> SpecialTagList();
        Task AddSpecialTag(SpecialTag_model stag);
        SpecialTag_model Get_specialTag(int? id);
        Task UpdateSpecialTag(SpecialTag_model modl);
        Task DeleteSpecialTag(SpecialTag_model modl);
    }
}
