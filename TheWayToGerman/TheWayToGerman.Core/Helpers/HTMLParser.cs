
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
    public IEnumerable<Guid> ReplaceImgSrcContent()
    {     
        var images = HtmlDoc.DocumentNode.SelectNodes("//img");
        foreach (var img in images)
        {           
            Guid imageReplacement = Guid.NewGuid();
            img.SetAttributeValue("src", imageReplacement.ToString());
            yield return imageReplacement;
        }       
    }
    public IEnumerable<string> GetImgSrcContent()
    {
        var images = HtmlDoc.DocumentNode.SelectNodes("//img");
        foreach (var img in images)
        {          
            yield return img.GetAttributeValue("src",null);
        }
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
