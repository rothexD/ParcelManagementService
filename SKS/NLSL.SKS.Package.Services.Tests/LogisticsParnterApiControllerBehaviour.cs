using System;

using AutoMapper;

using FakeItEasy;

using FluentAssertions;
using NUnit.Framework;
using NLSL.SKS.Package.Services.DTOs;
using NLSL.SKS.Package.Services.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using NLSL.SKS.Package.BusinessLogic.CustomExceptions;
using NLSL.SKS.Package.BusinessLogic.Interfaces;
using NLSL.SKS.Package.DataAccess.Sql.CustomExceptinos;

namespace NLSL.SKS.Package.Services.Tests
{
    public class LogisticsParnterApiControllerBehaviour 
    {
        private Parcel _testParcel = new();
        private Recipient _testSender = new();
        private Recipient _testRecipient = new();
        private LogisticsPartnerApiController _testController;
        private IParcelLogic _parcelLogic;
        private IMapper _mapper;
        private ILogger<LogisticsPartnerApiController> _logger;
        [SetUp]
        public void Setup()
        {
            _parcelLogic = A.Fake<IParcelLogic>();
            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<ILogger<LogisticsPartnerApiController>>();

            _testController = new LogisticsPartnerApiController(_parcelLogic, _mapper,_logger);
            
            _testParcel = new();
            _testSender = new();
            _testRecipient = new();

            _testSender.City = "testSender.City";
            _testSender.Country = "testSender.Country";
            _testSender.Name = "testSender.Name";
            _testSender.Street = "testSender.Street";
            _testSender.PostalCode = "testSender.PostalCode";

            _testRecipient.City = "testRecipient.City";
            _testRecipient.Country = "testRecipient.Country";
            _testRecipient.Name = "testRecipient.Name";
            _testRecipient.Street = "testRecipient.Street";
            _testRecipient.PostalCode = "testRecipient.PostalCode";

            _testParcel.Weight = 1;
            _testParcel.Sender = _testSender;
            _testParcel.Recipient = _testRecipient;
        }

        [Test]
        public void TransitionParcel_ValidParcel_Success()
        {
            ObjectResult result;
            A.CallTo(() => _parcelLogic.Submit(A<BusinessLogic.Entities.Parcel>.Ignored)).Returns(new BusinessLogic.Entities.Parcel());
            
            result = (ObjectResult) _testController.TransitionParcel(_testParcel, "ABCDEFGHI");

            result.StatusCode.Should().Be(200);
        }
        [Test]
        public void TransitionParcel_BadRequest_FromBL()
        {
            ObjectResult result;
            A.CallTo(() => _parcelLogic.Submit(A<BusinessLogic.Entities.Parcel>.Ignored)).Returns(null);
            
            result = (ObjectResult) _testController.TransitionParcel(_testParcel, "ABCDEFGHI");

            result.StatusCode.Should().Be(400);
        }
        
        [Test]
        public void TransitionParcel_BadRequest_FromBusinessLayerValidationException()
        {
            ObjectResult result;
            BusinessLayerExceptionBase exception = new BusinessLayerExceptionBase("test",new BusinessLayerValidationException());
            A.CallTo(() => _parcelLogic.Submit(A<BusinessLogic.Entities.Parcel>.Ignored))
                .Throws(exception);
            
            
            
            result = (ObjectResult) _testController.TransitionParcel(_testParcel, "ABCDEFGHI");

            result.StatusCode.Should().Be(400);
        }
        
        [Test]
        public void TransitionParcel_BadRequest_FromDataAccessException()
        {
            ObjectResult result;
            BusinessLayerExceptionBase exception = new BusinessLayerExceptionBase("test",new DataAccessExceptionBase());
            A.CallTo(() => _parcelLogic.Submit(A<BusinessLogic.Entities.Parcel>.Ignored))
                .Throws(exception);
            
            
            
            result = (ObjectResult) _testController.TransitionParcel(_testParcel, "ABCDEFGHI");

            result.StatusCode.Should().Be(400);
        }
        
        [Test]
        public void TransitionParcel_BadRequest_FromException()
        {
            ObjectResult result;
            A.CallTo(() => _parcelLogic.Submit(A<BusinessLogic.Entities.Parcel>.Ignored))
                .Throws<Exception>();
            
            result = (ObjectResult) _testController.TransitionParcel(_testParcel, "ABCDEFGHI");

            result.StatusCode.Should().Be(400);
        }
    }
}
