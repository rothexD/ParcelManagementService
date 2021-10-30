﻿using System.Collections.Generic;

using FakeItEasy;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using NLSL.SKS.Package.DataAccess.Entities;
using NLSL.SKS.Package.DataAccess.Sql;

using NUnit.Framework;

namespace NLSL.SKS.Package.DataAccess.Tests
{
    public class WarehouseRepositoryBehaviour
    {
        private WarehouseRepository _repository;
        private PackageContext _context;
        
        
        [SetUp]
        public void Setup()
        {
            DbContextOptions<PackageContext> options = new DbContextOptions<PackageContext>();
            _context = A.Fake<PackageContext>(x=> 
                                                  x.WithArgumentsForConstructor(() => new PackageContext(options)));
            _repository = new WarehouseRepository(_context);
            
            A.CallTo(() => _context.Database.EnsureCreated()).Returns(true);
        }
        [Test]
        public void Create_WarehouseIsValid_intId()
        {
            A.CallTo(() => _context.Warehouses).Returns(DbContextMock.GetQueryableMockDbSet(new List<Warehouse>()));
            A.CallTo(() => _context.Warehouses.Add(null)).WithAnyArguments().Returns(null);
            A.CallTo(() => _context.SaveChanges()).Returns(1);
            
            var newWarehouse = new Warehouse()
                            {
                                Code = "testWarehouse"
                            };

            
            var result = _repository.Create(newWarehouse);
            
            A.CallTo(() => _context.Warehouses.Add(null)).WithAnyArguments().MustHaveHappenedOnceExactly();
            A.CallTo(() => _context.SaveChanges()).MustHaveHappenedOnceExactly();
            result.Should().Be("testWarehouse");
        }
        [Test]
        public void Update_WarehouseIsValid_intId()
        {
            A.CallTo(() => _context.Warehouses).Returns(DbContextMock.GetQueryableMockDbSet(new List<Warehouse>()));
            A.CallTo(() => _context.Warehouses.Update(null)).WithAnyArguments().Returns(null);
            A.CallTo(() => _context.SaveChanges()).Returns(1);
            
            var newWarehouse = new Warehouse()
                               {
                                   Code = "testWarehouse"
                               };

            
            _repository.Update(newWarehouse);
            
            A.CallTo(() => _context.Warehouses.Update(null)).WithAnyArguments().MustHaveHappenedOnceExactly();
            A.CallTo(() => _context.SaveChanges()).MustHaveHappenedOnceExactly();
        }
        [Test]
        public void Remove_WarehouseIsValid_intId()
        {
            A.CallTo(() => _context.Warehouses).Returns(DbContextMock.GetQueryableMockDbSet(new List<Warehouse>()));
            A.CallTo(() => _context.Warehouses.Remove(null)).WithAnyArguments().Returns(null);
            A.CallTo(() => _context.SaveChanges()).Returns(1);
            
            var newWarehouse = new Warehouse()
                                {
                                    Code = "testWarehouse"
                                };

            
            _repository.Delete("testWarehouse");
            
            A.CallTo(() => _context.Warehouses.Remove(null)).WithAnyArguments().MustHaveHappenedOnceExactly();
            A.CallTo(() => _context.SaveChanges()).MustHaveHappenedOnceExactly();
        }
        [Test]
        public void GetWarehouseByCode_PackageExists_Package()
        {
            A.CallTo(() => _context.Warehouses).Returns(DbContextMock.GetQueryableMockDbSet(new List<Warehouse>()
                                                                                         {
                                                                                             new Warehouse()
                                                                                             {
                                                                                                 Code="testWarehouse1"
                                                                                             },
                                                                                             new Warehouse()
                                                                                             {
                                                                                                 Code="testWarehouse2"
                                                                                             }
                                                                                         }));

            var result = _repository.GetWarehouseByCode("testWarehouse1");

            result.Code.Should().Be("testWarehouse1");
        }
        [Test]
        public void GetWarehouseByCode_PackageDoesNotExist_null()
        {
            A.CallTo(() => _context.Warehouses).Returns(DbContextMock.GetQueryableMockDbSet(new List<Warehouse>()));

            var result =  _repository.GetWarehouseByCode("aaaaaaaaaaaaaa");
            
            result.Should().Be(null);
        }
        [Test]
        public void GetAllWarehouses_FullWarehouselist_listofwarehouses()
        {
            var example1 = new Warehouse()
                           {
                               Code = "testWarehouse1"
                           };
            var example2 = new Warehouse()
                           {
                               Code = "testWarehouse2"
                           };
            
            A.CallTo(() => _context.Warehouses).Returns(DbContextMock.GetQueryableMockDbSet(new List<Warehouse>()
                                                                                            {
                                                                                                example1,
                                                                                                example2
                                                                                            }));

            var result = _repository.GetAllWarehouses();

            result.Should().Contain(example1);
            result.Should().Contain(example2);
            result.Should().HaveCount(2);
        }
    }
}