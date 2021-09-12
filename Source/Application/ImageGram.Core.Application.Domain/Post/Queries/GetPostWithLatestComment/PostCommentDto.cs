namespace ImageGram.Core.Application.Domain.Post.Queries.GetPostWithLatestComment
{
    public class PostCommentDto
    {
        #region Properties

        public string CreatorName { get; set; }

        public string Content { get; set; }

        public string CreateDateTime { get; set; }

        #endregion
    }
}