using Core.DataKit;
using Core.DataKit.Result;
using Core.DataKit.ReturnWrapper;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TheWayToGerman.Core.Helpers.Interfaces;

namespace TheWayToGerman.Core.Helpers;

public class ArticlesHandler
{
    public HTMLParser HTMLParser { get; }

    public IStorage Storage { get; }

    public ArticlesHandler(HTMLParser htmlParser, IStorage storage)
    {
        HTMLParser = htmlParser;
        Storage = storage;
  
    }

   public State LoadArticleContent(string content)
   {
        try
        {
            return HTMLParser.ParseHtml(content);
        }
        catch (Exception ex)
        {
            return ex;
        }
       
   }
   public Result<string> GetArticleContent()
   {
        return HTMLParser.GetHtml();
   }
   protected async Task<Result<Dictionary<string, string>>> GetImageFromStorage(IEnumerable<string> imagesPaths)
    {
        Dictionary<string, string> images = new Dictionary<string, string>(imagesPaths.Count());
        foreach (var imagePath in imagesPaths)
        {
            var imageContent = await Storage.LoadAsync(imagePath);
            if (imageContent.ContainError())
            {
                return imageContent.GetError();
            }
            images.Add(imagePath, Encoding.UTF8.GetString(imageContent.GetData()));
        }
        return images;
    }
   public async Task<State> ReMergeImage()
   {
        try
        {
            var  storedImagesResult = await GetImageFromStorage(HTMLParser.GetImgSrcContent());
            if (storedImagesResult.ContainError())
            {
                return storedImagesResult.GetError();
            }
            var storedImages = storedImagesResult.GetData();
            foreach (var img in HTMLParser.GetImages())
            {
               var imageID = img.GetAttributeValue("src", null);
               img.SetAttributeValue("src",storedImages[imageID]);
            }
            return new OK();
        }
        catch (Exception ex)
        {
            return ex;
        }
       
    }
   public async Task<State> SeparateImage() 
   {
        int proccessedImageIndex = 0;
        //don't remove ToList, first method get img src and second method replace it, so they work on the same field if they are not consoldate to list and keep on being enumrable they will be race condition
        var imagesContents = HTMLParser.GetImgSrcContent().ToList();
        var imagesIDs = HTMLParser.ReplaceImgSrcContent().ToList();       
        try
        {        
            for (; proccessedImageIndex < imagesContents.Count(); proccessedImageIndex++)
            {
                string imageID = imagesIDs.ElementAt(proccessedImageIndex);             
                string imageContent = imagesContents.ElementAt(proccessedImageIndex);
                var imageBytes = Encoding.UTF8.GetBytes(imageContent);

                var result = await Storage.SaveAsync(imageBytes, imageID);                
                if (result.IsNotOk())
                {
                    throw result.GetError();
                }
            }
            return new OK();
        }
        catch (Exception ex)
        {
            // if failed to save file, then delete all the saved file and return error
            var state = await DeleteImages(imagesIDs.Take(proccessedImageIndex));
            state = state + ex;
            return state.GetError();
        }     
   }
    public async Task<State> DeleteOldImage()
    {
        try
        {
            var imagesIds = HTMLParser.GetImgSrcContent().ToList();
            await DeleteImages(imagesIds);
            return new OK();
        }
        catch (Exception ex)
        {
            return ex;
        }  
    }
    protected async Task<State> DeleteImages(IEnumerable<string> paths)
    {   
        State result = new OK();
        foreach (var image in paths)
        {
            var state= await Storage.DeleteAsync(image);
            if (state.IsNotOk()) 
            {
                result = result + state.GetError();
            };
        }
        return result;
    }
}
