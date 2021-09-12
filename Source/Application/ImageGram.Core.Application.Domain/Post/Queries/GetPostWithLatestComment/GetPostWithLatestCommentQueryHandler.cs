using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ImageGram.Core.Application.Commons;
using ImageGram.Core.Application.Services;
using ImageGram.Core.Domain.AggregateRoots.Post;
using MediatR;

namespace ImageGram.Core.Application.Domain.Post.Queries.GetPostWithLatestComment
{
    public class GetPostWithLatestCommentQueryHandler : IRequestHandler<GetPostWithLatestCommentQuery, BaseQueryResult<List<GetPostWithLatestCommentDto>>>
    {
        #region Fields

        private readonly IPostRepository _postRepository;
        private readonly IStorageService _storageService;

        #endregion

        #region Constructors

        public GetPostWithLatestCommentQueryHandler(IPostRepository postRepository, IStorageService storageService)
        {
            _postRepository = postRepository;
            _storageService = storageService;
        }

        #endregion

        #region Public Methods

        public async Task<BaseQueryResult<List<GetPostWithLatestCommentDto>>> Handle(GetPostWithLatestCommentQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseQueryResult<List<GetPostWithLatestCommentDto>>
            {
                Data = new List<GetPostWithLatestCommentDto>()
            };

            var posts = await _postRepository.GetWithCommentsAsync(request.StartIndex, request.Length);

            response.Data = posts.Select(item => new GetPostWithLatestCommentDto
            {
                Caption = item.Post.Caption,
                CreatorName = item.Post.Account.Name,
                ImageUrl = _storageService.GetFileUrl(item.Post.Image),
                CreateDateTime = item.Post.CreatedDateTime.ToString("dddd, dd-MMM-yy HH:mm"),
                Comments = item.Comments.Select(comment => new PostCommentDto
                {
                    Content = comment.Content,
                    CreatorName = comment.Account.Name,
                    CreateDateTime = comment.CreatedDateTime.ToString("dddd, dd-MMM-yy HH:mm")
                }).ToList()
            }).ToList();

            return response;
        }

        #endregion
        
    }
}