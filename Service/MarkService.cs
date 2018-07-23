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
    public interface IMarkService
    {
        IEnumerable<Marks> GetMarks();
        Marks GetMark(int? id);
        void CreateMark(Marks Mark);
        void EditMark(Marks Mark);
        void SaveMark();
        void DeleteMark(int id);
        void DeleteMark(int Pid, int Uid);
    }

    public class MarkService : IMarkService
    {
        private readonly IMarkRepository MarkRepository;
        private readonly IUnitOfWork unitOfWork;

        public MarkService(IMarkRepository MarkRepository, IUnitOfWork unitOfWork)
        {
            this.MarkRepository = MarkRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateMark(Marks Mark)
        {
            MarkRepository.Add(Mark);
        }

        public void EditMark(Marks Mark)
        {
            MarkRepository.Edit(Mark);
        }

        public Marks GetMark(int? id)
        {
            var Mark = MarkRepository.GetById(id);
            return Mark;
        }

        public IEnumerable<Marks> GetMarks()
        {
            var Marks = MarkRepository.GetAll();
            return Marks;
        }

        public void SaveMark()
        {
            unitOfWork.Commit();
        }
        public void DeleteMark(int id)
        {
            var Mark = MarkRepository.GetById(id);
            MarkRepository.Delete(Mark);
        }
        public void DeleteMark(int Pid, int Uid)
        {
            var Mark = MarkRepository.GetById(Pid, Uid);
            MarkRepository.Delete(Mark);
        }
    }
}
