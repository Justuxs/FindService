using FindService.EF.Context;
using FindService.EF;
using Microsoft.EntityFrameworkCore;
using FindService.Dto.User;
using FindService.Dto;
using FindService.Dto.City;
using FindService.Dto.PhotoDto;

namespace FindService.Services.CityService
{
    public class PhotoService
    {
        private readonly ApplicationDbContext _context;

        public PhotoService(ApplicationDbContext context)
        {
            _context = context;
        }

        internal async Task<PhotoDtoResponse?> GetMainAdvertisementPhotoAsync(Guid adid)
        {
            Guid photosId = await _context.AdvertisementPhotos.Where(e => e.AdvertisementId == adid && !e.isMain).Select(e => e.Id).FirstOrDefaultAsync();

            var photo = await _context.Photos
            .Where(e => photosId == e.Id)
            .Select(c => new PhotoDtoResponse
            {
                Id = c.Id,
                Base64 = c.Base64
            }).FirstOrDefaultAsync();

            return photo;
        }

        internal async Task<List<PhotoDtoResponse>> GetAllAdvertisementPhotosAsync(Guid adid)
        {
            List<Guid> photosIds = await _context.AdvertisementPhotos.Where(e => e.AdvertisementId == adid && !e.isMain).Select(e => e.Id).ToListAsync();
            var photos = await _context.Photos
            .Where(e => photosIds.Contains(e.Id))
            .Select(c => new PhotoDtoResponse
            {
                Id = c.Id,
                Base64 = c.Base64
            }).ToListAsync();

            return photos;
        }

        internal async void SaveAdPhotos(Photo mainPhoto, List<Photo> photos, Guid id)
        {
            await _context.Photos.AddAsync(mainPhoto);
            await _context.Photos.AddRangeAsync(photos);
            List<AdvertisementPhoto> advertisementPhotos = new List<AdvertisementPhoto>();
            advertisementPhotos.Add(
                new AdvertisementPhoto() { isMain = true, Position = 0, AdvertisementId = id , PhotoId = mainPhoto.Id}
            );
            int pos = 1;
            foreach (Photo photo in photos)
            {
                advertisementPhotos.Add(
                     new AdvertisementPhoto() { isMain = false, Position = pos, AdvertisementId = id, PhotoId = photo.Id }
                ); ;
                pos++;
            }
            await _context.AdvertisementPhotos.AddRangeAsync(advertisementPhotos);
            await _context.SaveChangesAsync();
        }
    }
}
