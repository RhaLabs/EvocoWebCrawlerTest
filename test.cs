/*
 * Created by SharpDevelop.
 * User: bcrawford
 * Date: 8/14/2014
 * Time: 9:32 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using EvocoWebCrawler;
using HtmlAgilityPack;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;

namespace EvocoWebCrawlerTest
{
  /// <summary>
  /// Description of MyClass.
  /// </summary>
  public class EvocoWebCrawlerTest
  {
    [Test]
    public void LoginTest ()
    {
      var creds = new Credentials();
      
      creds.Username = "";
      creds.Password = "";
      creds.UseCookies = true;
      
      var crawler = new HttpClient(creds);
      
      var docStream = crawler.Navigate("http://www.bldgportal.com/RFI/Application/Projects/ProjectList.aspx");
      
      crawler.LogMeOut();
      
      var navigator = new HtmlNodeNavigator(docStream);
      
      navigator.MoveToRoot();
      
      var iterator = navigator.CurrentNode.SelectNodes(@".//title");
      
      var node = iterator[0];
      
      Assert.That( node.InnerText, Is.EqualTo("\r\n\tConstruction RFI\r\n") );
    }
    
    [Test]
    public void GetRfiTest ()
    {
      var creds = new Credentials();
      
      creds.Username = "";
      creds.Password = "";
      creds.UseCookies = true;
      
      var crawler = new HttpClient(creds);
      
      var docStream = crawler.Navigate("http://www.bldgportal.com/RFI/Application/Projects/ProjectList.aspx");

      var success = crawler.TryNavigateStoreRfi(4162, 0, 109, out docStream);
      
      Assert.That(success, Is.True);
      
      var rfiAttachments = crawler.GetHttpFileAttachment(docStream);
      
      crawler.LogMeOut();
      
      Assert.That( rfiAttachments.Count, Is.AtLeast(1) );
    }
    
  }
  
  public class Credentials : EvocoWebCrawler.Credentials.CredentialInterface
  {
    public string Username { get; set; }

    public string Password { get; set; }

    public bool UseCookies { get; set; }
  }
}