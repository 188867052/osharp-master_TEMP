using System.ComponentModel.DataAnnotations;
using Agile.Core.Identity.Dtos;
using OSharp.Entity;
using Shouldly;
using Xunit;

namespace Agile.Core.Tests.Identity
{
    public class InputDtoValidateExtensionsTests
    {
        [Fact]
        public void UserValidate()
        {
            UserInputDto dto = new UserInputDto();
            ShouldThrowExtensions.ShouldThrow<ValidationException>(() => dto.Validate(), "fds");
            dto = new UserInputDto() { UserName = "1" };
            ShouldThrowExtensions.ShouldThrow<ValidationException>(() => dto.Validate(), "fds");
            dto = new UserInputDto() { UserName = "1234" };
            ShouldThrowExtensions.ShouldThrow<ValidationException>(() => dto.Validate(), "fds");
        }
    }
}
