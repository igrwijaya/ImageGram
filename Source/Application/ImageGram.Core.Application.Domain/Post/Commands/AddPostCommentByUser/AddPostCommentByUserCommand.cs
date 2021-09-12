using ImageGram.Core.Application.Commons;
using MediatR;

namespace ImageGram.Core.Application.Domain.Post.Commands.AddPostCommentByUser
{
    public class AddPostCommentByUserCommand : IRequest<BaseCommandResult>
    {
        #region Properties

        public int PostId { get; set; }

        public string Content { get; set; }

        #endregion
    }
}