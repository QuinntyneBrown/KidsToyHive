using Core.Entities;

namespace ContentService.Features.Contents
{
    public class ContentApiModel
    {        
        public int ContentId { get; set; }
        public string Name { get; set; }

        public static ContentApiModel FromContent(Content content)
        {
            var model = new ContentApiModel();
            model.ContentId = content.ContentId;
            model.Name = content.Name;
            return model;
        }
    }
}
