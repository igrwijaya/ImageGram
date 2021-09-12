using System.IO;
using FluentValidation;
using ImageGram.Core.Constant.Constant;
using ImageGram.Core.Constant.Entity;
using Microsoft.Extensions.Configuration;

namespace ImageGram.Core.Application.Domain.Post.Commands.CreatePostByUser
{
    public class CreatePostByUserCommandValidator : AbstractValidator<CreatePostByUserCommand>
    {
        private readonly IConfiguration _configuration;
        
        public CreatePostByUserCommandValidator(IConfiguration configuration)
        {
            _configuration = configuration;
            
            RuleFor(prop => prop.Caption)
                .MaximumLength(PostEntityConstant.ContentLength)
                .NotEmpty();

            RuleFor(prop => prop.ImageFile)
                .Must(((command, stream) => IsValidFileExtension(stream, command.ImageFileName)))
                .WithMessage("Invalid file extension (.png, .jpg, .bmp)");
        }

        private bool IsValidFileExtension(Stream file, string fileName)
        {
            if (file == null)
            {
                return true;
            }
            
            var allowedExtensionConfigValue = _configuration[ConfigurationConstant.AllowedImageExtension];
            var fileExtension = Path.GetExtension(fileName);
            
            if (string.IsNullOrEmpty(allowedExtensionConfigValue) || string.IsNullOrEmpty(fileExtension))
            {
                return false;
            }

            return allowedExtensionConfigValue.Contains(fileExtension.ToLower());
        }
    }
}