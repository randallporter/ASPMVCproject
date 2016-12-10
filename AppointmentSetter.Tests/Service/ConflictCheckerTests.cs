
using AppointmentSetter.DataAccess;
using AppointmentSetter.Models;
using AppointmentSetter.Service;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentSetter.Tests.Service
{
    [TestFixture]
    class ConflictCheckerTests
    {
        private ConflictChecker _conflictChecker;
        private User attender;
        private User setter;

        [SetUp]
        public void Setup()
        {
            attender = A.Fake<User>();
            attender.ID = 2;
            setter = A.Fake<User>();
            setter.ID = 2;

            var appt = new Appointment
            {
                StartDate = new DateTime(2016, 1, 1, 1, 0, 0),
                EndDate = new DateTime(2016, 1, 1, 3, 0, 0),
                appointmentAttender = attender,
                AppointmentSetter = setter,
                ID = 1
            };

            var list = new List<Appointment> { appt }.AsQueryable();



            var context = A.Fake<AppointmentDBContext>();
            var repoMock = A.Fake<IAppointmentRepository>();
            A.CallTo(() => repoMock.All).Returns(list); //Returns(set.AsQueryable<Appointment>());


            _conflictChecker = new ConflictChecker(repoMock);
        }

        [Test]
        public void attenderHasConflict()
        {
            var StartDate = new DateTime(2016, 1, 1, 1, 0, 0);
            var EndDate = new DateTime(2016, 1, 1, 2, 30, 0);

            var rtn = _conflictChecker.GetAttenderConflict(attender, StartDate, EndDate);
            Assert.AreEqual(rtn.ID, 1);
        }

        [Test]
        public void attenderHasConflictBottomEdge()
        {
            var StartDate = new DateTime(2016, 1, 1, 0, 0, 0);
            var EndDate = new DateTime(2016, 1, 1, 0, 0, 1);

            var rtn = _conflictChecker.GetAttenderConflict(attender, StartDate, EndDate);
            Assert.AreEqual(rtn.ID, 1);
        }

        [Test]
        public void attenderHasConflictUpperEdge()
        {
            var StartDate = new DateTime(2016, 1, 1, 3, 59, 0);
            var EndDate = new DateTime(2016, 1, 1, 4, 0, 0);

            var rtn = _conflictChecker.GetAttenderConflict(attender, StartDate, EndDate);
            Assert.AreEqual(rtn.ID, 1);
        }

        [Test]
        public void setterHasConflict()
        {
            var StartDate = new DateTime(2016, 1, 1, 1, 0, 0);
            var EndDate = new DateTime(2016, 1, 1, 2, 30, 0);

            var rtn = _conflictChecker.GetSetterConflict(setter, StartDate, EndDate);
            Assert.AreEqual(rtn.ID, 1);
        }
        [Test]
        public void setterHasConflictBottomEdge()
        {
            var StartDate = new DateTime(2016, 1, 1, 0, 0, 0);
            var EndDate = new DateTime(2016, 1, 1, 0, 0, 1);

            var rtn = _conflictChecker.GetSetterConflict(setter, StartDate, EndDate);
            Assert.AreEqual(rtn.ID, 1);
        }

        [Test]
        public void setterHasConflictUpperEdge()
        {
            var StartDate = new DateTime(2016, 1, 1, 3, 59, 0);
            var EndDate = new DateTime(2016, 1, 1, 4, 0, 0);

            var rtn = _conflictChecker.GetSetterConflict(setter, StartDate, EndDate);
            Assert.AreEqual(rtn.ID, 1);
        }

        [Test]
        public void attenderHasNoConflict()
        {
            var StartDate = new DateTime(2016, 2, 1, 3, 59, 0);
            var EndDate = new DateTime(2016, 2, 1, 4, 0, 0);

            var rtn = _conflictChecker.GetSetterConflict(attender, StartDate, EndDate);
            Assert.IsNull(rtn);
        }

        [Test]
        public void setterHasNoConflict()
        {
            var StartDate = new DateTime(2016, 2, 1, 3, 59, 0);
            var EndDate = new DateTime(2016, 2, 1, 4, 0, 0);

            var rtn = _conflictChecker.GetSetterConflict(setter, StartDate, EndDate);
            Assert.IsNull(rtn);
        }
    }
}
