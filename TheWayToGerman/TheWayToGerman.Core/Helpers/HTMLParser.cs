
using Core.DataKit;
using Core.DataKit.Result;
using Core.DataKit.ReturnWrapper;
using HtmlAgilityPack;
using System.Net.Http;

namespace TheWayToGerman.Core.Helpers;

public class HTMLParser
{   
    protected HtmlDocument HtmlDoc { get; } = new HtmlDocument();
    public HTMLParser()
    {
        
    }

    public State ParseHtml(string htmlContent)
    {
        try
        {
            HtmlDoc.LoadHtml(htmlContent);
            return new OK();
        }
        catch (Exception ex)
        {
            return ex;
        }
       
    }
    public IEnumerable<string> ReplaceImgSrcContent()
    {     
        var images = HtmlDoc.DocumentNode.SelectNodes("//img");
        foreach (var img in images)
        {           
            string imageReplacement = Guid.NewGuid().ToString();
            img.SetAttributeValue("src", imageReplacement);
            yield return imageReplacement;
        }       
    }
    public IEnumerable<string> GetImgSrcContent()
    {
        foreach (var img in GetImages())
        {          
            yield return img.GetAttributeValue("src",null);
        }
    }
    public IEnumerable<HtmlNode> GetImages()
    {
        return HtmlDoc.DocumentNode.SelectNodes("//img");
       
    }
    public Result<string> GetHtml()
    {
        try
        {
            using StringWriter writer = new StringWriter();
            HtmlDoc.Save(writer);
            return writer.ToString();
        }
        catch (Exception ex)
        {
            return ex;
        }     
        
    }
}
