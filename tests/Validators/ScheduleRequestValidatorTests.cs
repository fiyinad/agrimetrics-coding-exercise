using System.Collections.Generic;
using AutoFixture;
using FluentValidation.TestHelper;
using Orders.Models.Requests;
using Orders.Validators;
using Xunit;

namespace CustomerTests.Validators
{
  public class ScheduleRequestValidatorTests
  {
    private readonly Fixture _fixture = new Fixture();
    private readonly ScheduleRequestValidator _validator;

    public ScheduleRequestValidatorTests()
    {
      _validator = new ScheduleRequestValidator();
    }

    [Fact]
    public void ShouldErrorWhen_Orders_IsNull()
    {
      // arrange, 
      var model = new ScheduleRequest
      {
        Orders = null
      };
      
      // act & 
      var result = _validator.TestValidate(model);
      
      // assert 
      result.ShouldHaveValidationErrorFor(x => x.Orders);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    public void ShouldErrorWhen_Orders_CustomerName_IsInvalid(string customerName)
    {
      // arrange, 
      var model = new ScheduleRequest
      {
        Orders = new List<Order>
        {
          new Order { CustomerName = customerName }
        }
      };
      
      // act & 
      var result = _validator.TestValidate(model);
      
      // assert 
      result.ShouldHaveValidationErrorFor("Orders[0].CustomerName");
    }
  }
}