using Amazon.S3;
using Amazon.S3.Model;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands
{
    public class SetUserImageCommand : IRequest<string>
    {
        public Guid UserId { get; set; }
        public IFormFile? Image { get; set; }
    }

    public class SetUserImageCommandHandler : IRequestHandler<SetUserImageCommand, string>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SetUserImageCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<string> Handle(SetUserImageCommand request, CancellationToken cancellationToken)
        {
            string? imageLink = await SetImage(request.Image, request.UserId);

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            user.UserImageLink = imageLink;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return imageLink;
        }

        private async Task<string> SetImage(IFormFile Image, Guid UserId)
        {
            var config = new AmazonS3Config()
            {
                ServiceURL = "https://hb.ru-msk.vkcs.cloud"
            };
            var client = new AmazonS3Client("iKhpAXD4Z7WyEgb4LxBA4Q", "7HnfoW57oRZHa41PYH3Hq5C2AagiA9SfuWEGcFWNmkXz", config);
            var putObjectRequest = new PutObjectRequest
            {
                InputStream = Image.OpenReadStream(),
                BucketName = "notes_users_image",
                Key = $"{UserId}_{Image.FileName}",
                ContentType = Image.ContentType
            };
            var task = await client.PutObjectAsync(putObjectRequest);

            string? imageLink = null;
            if (task.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                imageLink = $"https://notes_users_image.hb.ru-msk.vkcs.cloud/{UserId}_{Image.FileName}";
            }

            return imageLink;
        }
    }
}
