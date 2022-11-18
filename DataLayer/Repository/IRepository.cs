using DataLayer.Models;
using DataLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public interface IRepository
    {
        List<TblCatagory> GetAllCategoriesWithRestprants();
        int GetAllCategoryCount();
        TblCatagory GetSingleCategory();

        HomeImageTextVm SelectConfigs();
        List<TblCatagory> CategoryGridShowInHome();
        List<TblDoc> GetAllVideo();
        List<TblCatagory> GetAllCategotyIcon();
    }
}
