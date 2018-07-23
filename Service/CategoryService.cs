using Data.Infrastructure;
using Data.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICategoryService
    {
        IEnumerable<Categories> GetCategories();
        Categories GetCategory(int? id);
        void CreateCategory(Categories Category);
        void EditCategory(Categories Category);
        void SaveCategory();
        void DeleteCategory(int id);
        //DbSet<Categories> GetTable();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository CategoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(ICategoryRepository CategoryRepository, IUnitOfWork unitOfWork)
        {
            this.CategoryRepository = CategoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateCategory(Categories Category)
        {
            CategoryRepository.Add(Category);
        }

        public void EditCategory(Categories Category)
        {
            CategoryRepository.Edit(Category);
        }

        public Categories GetCategory(int? id)
        {
            var Category = CategoryRepository.GetById(id);
            return Category;
        }

        //public DbSet<Categories> GetTable()
        //{
        //    var Category = CategoryRepository.GetTable();
        //    return Category;
        //}
        public IEnumerable<Categories> GetCategories()
        {
            var Categories = CategoryRepository.GetAll();
            return Categories;
        }

        public void SaveCategory()
        {
            unitOfWork.Commit();
        }
        public void DeleteCategory(int id)
        {
            var Category = CategoryRepository.GetById(id);
            CategoryRepository.Delete(Category);
        }
    }
}
