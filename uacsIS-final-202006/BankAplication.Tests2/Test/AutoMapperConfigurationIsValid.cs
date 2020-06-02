using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using BankAplication.Tests2.Internal;
using BankApplication.Data;
using BankApplication.Data.Models;
using BankApplication.Data.DTOs;


namespace BankApplication.Tests2.Test
{
    [TestFixture]
    public class AutoMapperConfigurationIsValid
    {
        [Test, Category("AutoMapper")]
        public void AutoMapper_Configuration_IsValid()
        {
            // Arrange
            var configuration = AutoMapperModule.CreateMapperConfiguration();

            // Act/Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
