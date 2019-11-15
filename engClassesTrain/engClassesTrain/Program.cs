using AutoMapper;
using System;
using System.Collections.Generic;

namespace engClassesTrain
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new UserRepository();

            // Настройка AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<User, IndexUserViewModel>());
            Mapper.Map<IEnumerable<User>, List<IndexUserViewModel>>(repo.GetAll());
            return View(users);
        }
    }
}
