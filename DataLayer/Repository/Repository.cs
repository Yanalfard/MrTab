using Castle.Core.Configuration;
using Dapper;
using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataLayer.Repository
{
    public class Repository : IRepository
    {
        private IDbConnection db;

        public Repository(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<TblCatagory> CategoryGridShowInHome()
        {
            string query = "Select Top 8 CatagoryId,Name,ImageUrl From TblCatagory " +
                "Where IsHome=1 And ImageUrl Is Not Null Order By CatagoryId Desc";
            return db.Query<TblCatagory>(query).ToList();
        }

        public List<TblCatagory> GetAllCategoriesWithRestprants()
        {
           
            string query = $"Select C.*,R.* FROM  TblCatagory AS C " +
                "INNER JOIN TblRestaurant AS R  ON R.CatagoryId = C.CatagoryId " +
                " Where IsHome=1 order by C.CatagoryId";
            //string query = "Select C.CatagoryId,C.Name , Count(*) FROM  TblCatagory AS C " +
            //    "INNER JOIN TblRestaurant AS R  ON R.CatagoryId = C.CatagoryId " +
            //    " where IsHome=1 Group By C.CatagoryId,C.Name order by Count(*) desc;";


            var companyDic = new Dictionary<int, TblCatagory>();

            var company = db.Query<TblCatagory, TblRestaurant, TblCatagory>(query, (c, e) =>
            {
                if (!companyDic.TryGetValue(c.CatagoryId, out var currentCompany))
                {
                    currentCompany = c;
                    companyDic.Add(currentCompany.CatagoryId, currentCompany);
                }

                currentCompany.TblRestaurant.Add(e);
                return currentCompany;
            }, splitOn: "RestaurantId");

            return company.Distinct().ToList();
        }

        public int GetAllCategoryCount()
        {
            string queryCountCategory = "Select Count(*) From TblCatagory";
            return  db.Query<int>(queryCountCategory).Single() - 1;
        }

        public List<TblCatagory> GetAllCategotyIcon()
        {
            string query = "Select Top 8 CatagoryId,Name,IconUrl From TblCatagory " +
               "Where IsHome=1 Order By CatagoryId Desc";
            return db.Query<TblCatagory>(query).ToList();
        }

        public List<TblDoc> GetAllVideo()
        {
            string query = "Select * From TblDoc " +
               "Order By DocId Desc";
            return db.Query<TblDoc>(query).ToList();
        }

        public TblCatagory GetSingleCategory()
        {
            string selectCategoryId = "DECLARE @CatagoryId INT Select Top 1 @CatagoryId=C.CatagoryId FROM  TblCatagory AS C INNER JOIN TblRestaurant AS R  ON R.CatagoryId = C.CatagoryId  where IsHome=1 Group By C.CatagoryId order by Count(*) Asc ,C.CatagoryId DESC ; Select @CatagoryId AS CatagoryId";
            int categoryId= db.Query<int>(selectCategoryId).Single();
            var param = new
            {
                CatagoryId = categoryId
            };

            var query = "SELECT * FROM TblCatagory WHERE CatagoryId= @CatagoryId SELECT * FROM TblRestaurant WHERE CatagoryId= @CatagoryId";
            TblCatagory company;

            using (var lists = db.QueryMultiple(query, param))
            {
                company = lists.Read<TblCatagory>().ToList().FirstOrDefault();
                company.TblRestaurant = lists.Read<TblRestaurant>().ToList();
            }


            return company;

        }

        public HomeImageTextVm SelectConfigs()
        {
            string query1 = "Select Value From TblConfig Where Keyword =N'MainBanner'";
            string query2 = "Select Value From TblConfig Where Keyword =N'MainColor'";
            string query3 = "Select Value From TblConfig Where Keyword =N'MainImage'";
            string query4 = "Select Value From TblConfig Where Keyword =N'MainText'";
            string query5 = "Select Value From TblConfig Where Keyword =N'MobileAppBackGroundImage'";
            string query6 = "Select Value From TblConfig Where Keyword =N'MobileAppBackGroundText'";
            string query7 = "Select Value From TblConfig Where Keyword =N'UkLiColor'";
            string query8 = "Select Value From TblConfig Where Keyword =N'TextSlider'";
            string query9 = "Select Value From TblConfig Where Keyword =N'LocationAndSearch'";


            HomeImageTextVm homeVm = new HomeImageTextVm();
            homeVm.MainBanner = db.Query<string>(query1).Single();
            homeVm.MainColor = db.Query<string>(query2).Single();
            homeVm.MainImage = db.Query<string>(query3).Single();
            homeVm.MainText = db.Query<string>(query4).Single();
            homeVm.MobileAppBackGroundImage = db.Query<string>(query5).Single();
            homeVm.MobileAppBackGroundText = db.Query<string>(query6).Single();
            homeVm.UkLiColor = db.Query<string>(query7).Single();
            homeVm.TextSlider = db.Query<string>(query8).Single();
            homeVm.LocationAndSearch = db.Query<string>(query9).Single();
            return homeVm;
        }
    }
}
