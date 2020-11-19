using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using Services.Repositories;

namespace Services.Services
{
    public class Core : IDisposable
    {
        private readonly MrTabContext _context = new MrTabContext();
        private MainRepo<TblReport> _report;
        private MainRepo<TblCity> _city;
        private MainRepo<TblCatagory> _catagory;
        private MainRepo<TblRestaurant> _restaurant;
        private MainRepo<TblImage> _image;
        private MainRepo<TblFoodType> _foodType;
        private MainRepo<TblFood> _food;
        private MainRepo<TblProperty> _property;
        private MainRepo<TblRole> _role;
        private MainRepo<TblUser> _rser;
        private MainRepo<TblComment> _comment;
        private MainRepo<TblWorkTime> _workTimes;
        private MainRepo<TblMealType> _mealType;

        public MainRepo<TblReport> Report => _report ??= new MainRepo<TblReport>(_context);
        public MainRepo<TblCity> City => _city ??= new MainRepo<TblCity>(_context);
        public MainRepo<TblCatagory> Catagory => _catagory ??= new MainRepo<TblCatagory>(_context);
        public MainRepo<TblRestaurant> Restaurant => _restaurant ??= new MainRepo<TblRestaurant>(_context);
        public MainRepo<TblImage> Image => _image ??= new MainRepo<TblImage>(_context);
        public MainRepo<TblFoodType> FoodType => _foodType ??= new MainRepo<TblFoodType>(_context);
        public MainRepo<TblFood> Food => _food ??= new MainRepo<TblFood>(_context);
        public MainRepo<TblProperty> Property => _property ??= new MainRepo<TblProperty>(_context);
        public MainRepo<TblRole> Role => _role ??= new MainRepo<TblRole>(_context);
        public MainRepo<TblUser> User => _rser ??= new MainRepo<TblUser>(_context);
        public MainRepo<TblComment> Comment => _comment ??= new MainRepo<TblComment>(_context);
        public MainRepo<TblWorkTime> WorkTime => _workTimes ??= new MainRepo<TblWorkTime>(_context);
        public MainRepo<TblMealType> MealType => _mealType ??= new MainRepo<TblMealType>(_context);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
