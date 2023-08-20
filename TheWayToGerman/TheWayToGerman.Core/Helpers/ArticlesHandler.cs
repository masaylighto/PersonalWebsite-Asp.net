using Core.DataKit;
using Core.DataKit.Result;
using Core.DataKit.ReturnWrapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using TheWayToGerman.Core.Configuration;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Helpers.Interfaces;
using TheWayToGerman.Core.Loggers;

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
   
   public async Task<Result<IEnumerable<Guid>>> SeparateImage() 
   {
        int proccessedImageIndex = 0;
        var imagesContents = HTMLParser.GetImgSrcContent();
        var imagesIDs = HTMLParser.ReplaceImgSrcContent();       
        try
        {        
            for (; proccessedImageIndex < imagesContents.Count(); proccessedImageIndex++)
            {
                string imageID = imagesIDs.ElementAt(proccessedImageIndex).ToString();             
                string imageContent = imagesContents.ElementAt(proccessedImageIndex);
                var imageBytes = Encoding.UTF8.GetBytes(imageContent);
                var result = await Storage.SaveAsync(imageBytes, imageID);                
                if (result.IsNotOk())
                {
                    throw result.GetError();
                }
            }
            return Result.From(imagesIDs);
        }
        catch (Exception ex)
        {
            // if failed to save file, then delete all the saved file and return error
            var state = await DeleteImages(imagesIDs.Take(proccessedImageIndex).Select(x=>x.ToString()));
            state = state + ex;
            return state.GetError();
        }     
   }
    public async Task<State> DeleteImages(IEnumerable<string> paths)
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
