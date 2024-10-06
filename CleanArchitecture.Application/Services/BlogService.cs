using AutoMapper;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Enitites;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository ?? throw new ArgumentNullException(nameof(blogRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Blog> CreateAsync(BlogCreateDto blogDto)
        {
            if (blogDto == null)
                throw new ArgumentNullException(nameof(blogDto));

            var blog = _mapper.Map<Blog>(blogDto);

            return await _blogRepository.CreateAsync(blog);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _blogRepository.DeleteAsync(id);
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await _blogRepository.GetAllAsync();
        }

        public async Task<Blog> GetByIdAsync(Guid id)
        {
            return await _blogRepository.GetByIdAsync(id);
        }

        public async Task<Blog> UpdateAsync(BlogUpdateDto blogDto)
        {
            if (blogDto == null)
                throw new ArgumentNullException(nameof(blogDto));

            var blog = _mapper.Map<Blog>(blogDto);

            return await _blogRepository.UpdateAsync(blog);
        }
    }
}
