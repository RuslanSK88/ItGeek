using ItGeek.BLL;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using System.Xml;

namespace ItGeek.Web.Controllers;

public class RobotsController : Controller
{
    private readonly UnitOfWork _uow;

    public RobotsController(UnitOfWork uow)
    {
        _uow = uow;
    }

    [Route("/robots.txt")]
    public string RobotTxt()
    {
        //User-agent: *
        //Disallow: ''
        //Sitemap: https://   /sitemap.xml
        var sb = new StringBuilder();
        sb.AppendLine("User-agent: *");
        sb.AppendLine("Disallow: ''");
        sb.AppendLine("Sitemap: https://MySite/sitemap.xml");

        return sb.ToString();
    }

    [Route("/sitemap.xml")]
    public async Task SitemapXml()
    {
        var host = "https://localhost:7013/category/";

        Response.ContentType = "application/xml";
        using var xml = XmlWriter.Create(this.Response.Body, new XmlWriterSettings { Indent = true });
        xml.WriteStartDocument();
        xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

        var posts = await _uow.PostRepository.ListWithIncludeAllAsync();

        foreach (var post in posts)
        {
            xml.WriteStartElement("url");
            xml.WriteElementString("loc", host + post.Categories.FirstOrDefault().Slug + "/" + post.Slug);
            xml.WriteElementString("lastmod", post.EditedAt.ToString("yyyy-MM-ddThh:mmzzz", CultureInfo.InvariantCulture));
            xml.WriteEndElement();
        }

        xml.WriteEndElement();
    }
}
