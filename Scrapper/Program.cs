using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Scrapper
{
    internal class Program
    {
        public object lockOb = new object();
        private static void Main(string[] args)
        {
            var program = new Program();
            program.mergeGlassDoorCompaniesFiles();
            //program.ScrapGlassDoorCompanies();

            //var interviewData = JsonConvert.DeserializeObject<InterviewModel[]>(File.ReadAllText(@"C:\Users\Ashish\Desktop\Interviews(Ambition).json"));
        }

        private void mergeGlassDoorCompaniesFiles()
        {
            var files = Directory.GetFiles(@"C:\Users\Ashish\Desktop\", "Companies(Ambition)*.json");
            var companyList = new List<CompanyModel>();

            foreach (var file in files)
            {
                var list = JsonConvert.DeserializeObject<CompanyModel[]>(File.ReadAllText(file));
                companyList.AddRange(list);
            }

            var json = JsonConvert.SerializeObject(companyList);
            File.WriteAllText(@"C:\Users\Ashish\Desktop\Companies(Ambition)Final.json", json);
        }

        private void ScrapGlassDoorCompanies()
        {
            var companyList = new ConcurrentBag<CompanyModel>();

            var companyUrls = new List<string>();
            var scrapped = new List<string>();

            if (File.Exists(@"C:\Users\Ashish\Desktop\CompanyUrls.txt"))
            {
                var allUrls = File.ReadAllLines(@"C:\Users\Ashish\Desktop\CompanyUrls.txt").ToList();
                var scrappedUrls = new List<string>();

                if(File.Exists(@"C:\Users\Ashish\Desktop\Companies(Ambition)ScrappedUrls.txt"))
                    scrappedUrls = File.ReadAllLines(@"C:\Users\Ashish\Desktop\Companies(Ambition)ScrappedUrls.txt").ToList();

                companyUrls = allUrls.Except(scrappedUrls).ToList();
            }
            else
            {
                Parallel.For(1, 8306, (i) =>
                {
                    var browser = new HtmlWeb();
                    var url = "https://www.glassdoor.co.in/Reviews/india-reviews-SRCH_IL.0,5_IN115.htm";
                    if (i > 1)
                    {
                        url = "https://www.glassdoor.co.in/Reviews/india-reviews-SRCH_IL.0,5_IN115_IP" + i + ".htm";
                    }

                    var success = false;
                    do
                    {
                        try
                        {
                            var comapniesDoc = browser.Load(url);
                            var companies = comapniesDoc.DocumentNode.SelectNodes("//div[contains(@class,'eiHdrModule module snug')]");
                            foreach (var company in companies)
                            {
                                var companyLink = company.SelectSingleNode(".//a[contains(@class,'sqLogoLink')]");
                                companyUrls.Add("https://www.glassdoor.co.in" + companyLink.Attributes["href"].Value);
                            }
                            success = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error Occured, Retrying in 1 sec ...");
                            Thread.Sleep(1000);
                        }
                    }
                    while (!success);
                });

                File.WriteAllLines(@"C:\Users\Ashish\Desktop\CompanyUrls.txt", companyUrls);
            }

            Parallel.ForEach(companyUrls, (url) =>
             {
                 var success = false;
                 var browser = new HtmlWeb();
                 var company = new CompanyModel();
                 HtmlDocument doc = null;
                 do
                 {
                     try
                     {
                         doc = browser.Load(url);
                         var companyData = doc.DocumentNode.SelectNodes("//div[contains(@class,'infoEntity')]");

                         company.Name = doc.DocumentNode.SelectSingleNode("//h1[contains(@class,'strong tightAll')]").InnerText.Trim();
                         var logo = doc.DocumentNode.SelectSingleNode("//span[contains(@class,'lgSqLogo')]/img")?.Attributes["src"].Value.Trim();

                         if (!string.IsNullOrWhiteSpace(logo))
                         {
                             company.Logo = company.Name.Replace(" ", "-").Replace("|", "") + Path.GetExtension(logo);
                             company.LogoUrl = logo;

                             var fileName = @"C:\Users\Ashish\Desktop\LogosAmbition\" + company.Logo;
                             if (!File.Exists(fileName))
                             {
                                 using (var client = new WebClient())
                                 {
                                     client.DownloadFile(logo, @"C:\Users\Ashish\Desktop\LogosAmbition\" + company.Logo);
                                 }
                             }
                         }

                         foreach (var data in companyData)
                         {
                             if (data.SelectSingleNode("./label").InnerText == "Website")
                             {
                                 company.Website = data.SelectSingleNode("./span").InnerText.Trim();
                             }
                             else if (data.SelectSingleNode("./label").InnerText == "Headquarters")
                             {
                                 company.Headquarters = data.SelectSingleNode("./span").InnerText.Trim();
                             }
                             else if (data.SelectSingleNode("./label").InnerText == "Size")
                             {
                                 company.Size = data.SelectSingleNode("./span").InnerText.Trim();
                             }
                             else if (data.SelectSingleNode("./label").InnerText == "Founded")
                             {
                                 company.Founded = data.SelectSingleNode("./span").InnerText.Trim();
                             }
                             else if (data.SelectSingleNode("./label").InnerText == "Industry")
                             {
                                 company.Industry = data.SelectSingleNode("./span").InnerText.Trim();
                             }
                         }
                         success = true;
                     }
                     catch (Exception e)
                     {
                         Console.WriteLine("Error Occured, Retrying in 2 sec ...");
                         Thread.Sleep(2000);
                     }
                 }
                 while (!success);

                 company.Description = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'margTop empDescription')]")?.Attributes["data-full"].Value.Trim();

                 lock (lockOb)
                 {
                     companyList.Add(company);
                     scrapped.Add(url);

                     if (companyList.Count >= 2622)
                     {
                         var json = JsonConvert.SerializeObject(companyList);
                         File.WriteAllText(@"C:\Users\Ashish\Desktop\Companies(Ambition)" + DateTime.Now.Ticks + ".json", json);
                         File.AppendAllLines(@"C:\Users\Ashish\Desktop\Companies(Ambition)ScrappedUrls.txt", scrapped);

                         companyList.Clear();
                         scrapped.Clear();
                     }
                 }
                 Console.WriteLine("Scrapped: " + url);
             });
        }

        //private void ScrapAmbitionCompanies()
        //{
        //    var companyList = new ConcurrentBag<CompanyModel>();

        //    var companyUrls = new List<string>();
        //    var scrapped = new List<string>();

        //    if (File.Exists(@"C:\Users\Ashish\Desktop\CompanyUrls.txt"))
        //    {
        //        var allUrls = File.ReadAllLines(@"C:\Users\Ashish\Desktop\CompanyUrls.txt").ToList();
        //        var scrappedUrls = new List<string>();

        //        if (File.Exists(@"C:\Users\Ashish\Desktop\Companies(Ambition)ScrappedUrls.txt"))
        //            scrappedUrls = File.ReadAllLines(@"C:\Users\Ashish\Desktop\Companies(Ambition)ScrappedUrls.txt").ToList();

        //        companyUrls = allUrls.Except(scrappedUrls).ToList();
        //    }
        //    else
        //    {
        //        Parallel.For(1, 8306, (i) =>
        //        {
        //            var browser = new HtmlWeb();
        //            var url = "https://www.glassdoor.co.in/Reviews/india-reviews-SRCH_IL.0,5_IN115.htm";
        //            if (i > 1)
        //            {
        //                url = "https://www.glassdoor.co.in/Reviews/india-reviews-SRCH_IL.0,5_IN115_IP" + i + ".htm";
        //            }

        //            var success = false;
        //            do
        //            {
        //                try
        //                {
        //                    var comapniesDoc = browser.Load(url);
        //                    var companies = comapniesDoc.DocumentNode.SelectNodes("//div[contains(@class,'eiHdrModule module snug')]");
        //                    foreach (var company in companies)
        //                    {
        //                        var companyLink = company.SelectSingleNode(".//a[contains(@class,'sqLogoLink')]");
        //                        companyUrls.Add("https://www.glassdoor.co.in" + companyLink.Attributes["href"].Value);
        //                    }
        //                    success = true;
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine("Error Occured, Retrying in 1 sec ...");
        //                    Thread.Sleep(1000);
        //                }
        //            }
        //            while (!success);
        //        });

        //        File.WriteAllLines(@"C:\Users\Ashish\Desktop\CompanyUrls.txt", companyUrls);
        //    }

        //    Parallel.ForEach(companyUrls, (url) =>
        //    {
        //        var success = false;
        //        var browser = new HtmlWeb();
        //        var company = new CompanyModel();
        //        HtmlDocument doc = null;
        //        do
        //        {
        //            try
        //            {
        //                doc = browser.Load(url);
        //                var companyData = doc.DocumentNode.SelectNodes("//div[contains(@class,'infoEntity')]");

        //                company.Name = doc.DocumentNode.SelectSingleNode("//h1[contains(@class,'strong tightAll')]").InnerText.Trim();
        //                var logo = doc.DocumentNode.SelectSingleNode("//span[contains(@class,'lgSqLogo')]/img")?.Attributes["src"].Value.Trim();

        //                if (!string.IsNullOrWhiteSpace(logo))
        //                {
        //                    company.Logo = company.Name.Replace(" ", "-").Replace("|", "") + Path.GetExtension(logo);
        //                    company.LogoUrl = logo;

        //                    var fileName = @"C:\Users\Ashish\Desktop\LogosAmbition\" + company.Logo;
        //                    if (!File.Exists(fileName))
        //                    {
        //                        using (var client = new WebClient())
        //                        {
        //                            client.DownloadFile(logo, @"C:\Users\Ashish\Desktop\LogosAmbition\" + company.Logo);
        //                        }
        //                    }
        //                }

        //                foreach (var data in companyData)
        //                {
        //                    if (data.SelectSingleNode("./label").InnerText == "Website")
        //                    {
        //                        company.Website = data.SelectSingleNode("./span").InnerText.Trim();
        //                    }
        //                    else if (data.SelectSingleNode("./label").InnerText == "Headquarters")
        //                    {
        //                        company.Headquarters = data.SelectSingleNode("./span").InnerText.Trim();
        //                    }
        //                    else if (data.SelectSingleNode("./label").InnerText == "Size")
        //                    {
        //                        company.Size = data.SelectSingleNode("./span").InnerText.Trim();
        //                    }
        //                    else if (data.SelectSingleNode("./label").InnerText == "Founded")
        //                    {
        //                        company.Founded = data.SelectSingleNode("./span").InnerText.Trim();
        //                    }
        //                    else if (data.SelectSingleNode("./label").InnerText == "Industry")
        //                    {
        //                        company.Industry = data.SelectSingleNode("./span").InnerText.Trim();
        //                    }
        //                }
        //                success = true;
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine("Error Occured, Retrying in 2 sec ...");
        //                Thread.Sleep(2000);
        //            }
        //        }
        //        while (!success);

        //        company.Description = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'margTop empDescription')]")?.Attributes["data-full"].Value.Trim();

        //        lock (lockOb)
        //        {
        //            companyList.Add(company);
        //            scrapped.Add(url);

        //            if (companyList.Count >= 2622)
        //            {
        //                var json = JsonConvert.SerializeObject(companyList);
        //                File.WriteAllText(@"C:\Users\Ashish\Desktop\Companies(Ambition)" + DateTime.Now.Ticks + ".json", json);
        //                File.AppendAllLines(@"C:\Users\Ashish\Desktop\Companies(Ambition)ScrappedUrls.txt", scrapped);

        //                companyList.Clear();
        //                scrapped.Clear();
        //            }
        //        }
        //        Console.WriteLine("Scrapped: " + url);
        //    });
        //}

        private void ScrapGlassDoor()
        {
            var reviewUrls = new List<string>();

            var browser = new HtmlWeb();
            foreach (var url in reviewUrls)
            {
                var doc = browser.Load(url);
                var interviewPageLink = doc.DocumentNode.SelectSingleNode("//a[contains(@class,'eiCell cell interviews')]");
                var interviewPage = browser.Load(interviewPageLink.Attributes["href"].Value);

                var interviews = interviewPage.DocumentNode.SelectNodes("//div[contains(@class,'empReview')]");

                var interviewsResult = new List<InterviewModel>();
                foreach (var interview in interviews)
                {
                    var model = new InterviewModel
                    {
                        PostedOn = interview.SelectSingleNode("//time[contains(@class,'date subtle')]").InnerText.Trim(),
                        AboutEmployee = interview.SelectSingleNode("//div[contains(@class,'author')]").InnerText.Trim(),
                        Description = interview.SelectSingleNode("//p[contains(@class,'interviewDetails')]").InnerText.Trim(),

                    };
                }
            }
        }

        private void ScrapAmbitionBoxInterviews()
        {
            var browser = new HtmlWeb();
            var interviewList = new List<InterviewModel>();

            var interviewUrls = new List<string>();

            if (File.Exists(@"C:\Users\Ashish\Desktop\InterviewUrls.txt"))
            {
                interviewUrls = File.ReadAllLines(@"C:\Users\Ashish\Desktop\InterviewUrls.txt").ToList();
            }
            else
            {
                for (int i = 1; i < 150; i++)
                {
                    var url = "https://www.ambitionbox.com/interviews/companies" + "?page=" + i;
                    var comapniesDoc = browser.Load(url);
                    var companies = comapniesDoc.DocumentNode.SelectNodes("//div[contains(@class,'company_tile_wrap')]");
                    foreach (var company in companies)
                    {
                        var interviewLink = company.SelectSingleNode(".//div[contains(@class,'company_logo')]/a");
                        interviewUrls.Add(interviewLink.Attributes["href"].Value);
                        interviewUrls.Add(interviewLink.Attributes["href"].Value + "?page=2");
                        interviewUrls.Add(interviewLink.Attributes["href"].Value + "?page=3");
                    }
                }
            }

            foreach (var url in interviewUrls)
            {
                var doc = browser.Load(url);
                var companyName = doc.DocumentNode.SelectSingleNode("//p[contains(@class,'h1')]")?.InnerText.Trim();
                if (string.IsNullOrWhiteSpace(companyName))
                {
                    continue;
                }

                var website = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'company-stats')]//tr[1]//tr[1]//a")?.Attributes["href"].Value.Trim();
                var interviews = doc.DocumentNode.SelectNodes("//*[@id='reviewsContainer']/article");

                if (interviews == null)
                {
                    continue;
                }

                foreach (var interview in interviews)
                {
                    var model = new InterviewModel
                    {
                        JobTitle = interview.SelectSingleNode(".//h2[contains(@class,'review-title')]/a")?.InnerText.Trim(),
                        AboutEmployee = interview.SelectSingleNode(".//div[contains(@class,'user-id')]").InnerText.Trim(),
                        PostedOn = interview.SelectSingleNode(".//div[contains(@class,'time meta-data')]//time").InnerText.Trim(),
                        Company = companyName,
                        WebSite = website,
                        Rounds = new List<InterviewRoundModel>()
                    };

                    var overallExp = interview.SelectNodes(".//p[contains(@class,'overall_experience_text')]");
                    if (overallExp != null && overallExp.Count > 0)
                    {
                        model.OverAllExp = overallExp[0].InnerText.Trim();
                    }

                    var desc = interview.SelectNodes(".//p[contains(@class,'job_source_text')]");
                    if (desc != null && desc.Count > 0)
                    {
                        model.Description = desc[0].InnerText.Trim();
                    }

                    var rounds = interview.SelectNodes(".//div[contains(@class,'interview_round_wrap')]");
                    if (rounds != null)
                    {
                        foreach (var round in rounds)
                        {
                            var roundModel = new InterviewRoundModel
                            {
                                Name = round.SelectSingleNode("./h3").InnerText.Trim(),
                                Questions = new List<QuestionModel>()
                            };

                            var description = round.SelectSingleNode(".//p[contains(@class,'row_description')]");
                            if (description != null)
                            {
                                roundModel.Description = description.InnerText.Trim();
                            }
                            else
                            {
                                var questions = round.SelectNodes(".//div[contains(@class,'row_description')]//ul[contains(@class,'questions')]//a");
                                if (questions != null && questions.Count > 0)
                                {
                                    foreach (var ques in questions)
                                    {
                                        var quesModel = new QuestionModel
                                        {
                                            Desc = ques.Attributes["title"].Value.Trim()
                                        };
                                        roundModel.Questions.Add(quesModel);
                                    }
                                }
                            }

                            model.Rounds.Add(roundModel);
                        }
                    }
                    interviewList.Add(model);
                }

                System.Console.WriteLine("Scrapped: " + url);
            }

            var data = JsonConvert.SerializeObject(interviewList);
            File.WriteAllText(@"C:\Users\Ashish\Desktop\Interviews(Ambition).json", data);
        }

        private void ScrapAmbitionBoxReviews()
        {
            var browser = new HtmlWeb();
            var reviewList = new List<ReviewModel>();

            var reviewUrls = new List<string>();

            if (File.Exists(@"C:\Users\Ashish\Desktop\ReviewUrls.txt"))
            {
                reviewUrls = File.ReadAllLines(@"C:\Users\Ashish\Desktop\ReviewUrls.txt").ToList();
            }
            else
            {
                for (int i = 1; i <= 1000; i++)
                {
                    var url = "https://www.ambitionbox.com/reviews?page=" + i + "&utm_source=naukri&utm_medium=gnb";
                    var comapniesDoc = browser.Load(url);
                    var companies = comapniesDoc.DocumentNode.SelectNodes("//div[contains(@class,'company_tile_wrap')]");
                    foreach (var company in companies)
                    {
                        var reviewLink = company.SelectSingleNode(".//div[contains(@class,'company_logo')]/a");
                        reviewUrls.Add(reviewLink.Attributes["href"].Value);
                        reviewUrls.Add(reviewLink.Attributes["href"].Value + "?page=2");
                        reviewUrls.Add(reviewLink.Attributes["href"].Value + "?page=3");
                    }
                }
            }

            foreach (var url in reviewUrls)
            {
                var doc = browser.Load(url);
                var companyName = doc.DocumentNode.SelectSingleNode("//p[contains(@class,'h1')]")?.InnerText.Trim();
                var reviews = doc.DocumentNode.SelectNodes("//*[@id='reviewsContainer']/article");

                if (reviews == null)
                {
                    continue;
                }

                foreach (var review in reviews)
                {
                    var model = new ReviewModel
                    {
                        AboutEmployee = review.SelectSingleNode(".//span[contains(@class,'misc_text_mobile')]").InnerText.Trim(),
                        PostedOn = review.SelectSingleNode(".//div[contains(@class,'time_wrap')]//time").InnerText.Trim(),
                        Company = companyName,
                        Rating = review.SelectSingleNode(".//div[contains(@class,'rate-wrap')]").Attributes["title"].Value.Replace("Rating", "").Trim()
                    };

                    var reviewTexts = review.SelectNodes(".//div[contains(@class,'review_text_wrap')]");
                    if (reviewTexts == null)
                    {
                        continue;
                    }

                    foreach (var text in reviewTexts)
                    {
                        if (string.IsNullOrWhiteSpace(text.InnerText))
                        {
                            continue;
                        }

                        if (text.SelectSingleNode(".//*[contains(@class,'review_text_title')]").InnerText.Trim() == "Likes")
                        {
                            model.Pros = getReviewText(text);
                        }
                        else if (text.SelectSingleNode(".//*[contains(@class,'review_text_title')]").InnerText.Trim() == "Dislikes")
                        {
                            model.Cons = getReviewText(text);
                        }
                        else if (text.SelectSingleNode(".//*[contains(@class,'review_text_title')]").InnerText.Trim() == "Work")
                        {
                            model.Description = getReviewText(text);
                        }
                    }
                    reviewList.Add(model);
                }
                System.Console.WriteLine("Scrapped: " + url);
            }
            var data = JsonConvert.SerializeObject(reviewList);
            File.WriteAllText(@"C:\Users\Ashish\Desktop\Reviews(Ambition).json", data);
        }

        private string getReviewText(HtmlNode text)
        {
            var node = text.SelectSingleNode(".//p");

            if (string.IsNullOrWhiteSpace(node.InnerText))
            {
                return string.Empty;
            }

            return node.ChildNodes[0].InnerText.Trim()
                + (node.ChildNodes.Count > 2 ? node.ChildNodes[2].InnerText.Trim() : string.Empty);
        }

    }

    internal class InterviewModel
    {
        public string PostedOn { get; set; }
        public string AboutEmployee { get; set; }
        public string Description { get; set; }
        public string OverAllExp { get; set; }
        public string JobTitle { get; set; }
        public List<InterviewRoundModel> Rounds { get; set; }
        public string Company { get; set; }
        public string WebSite { get; set; }
    }

    internal class InterviewRoundModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }

    internal class QuestionModel
    {
        public string Desc { get; set; }
    }

    internal class ReviewModel
    {
        public string AboutEmployee { get; set; }
        public string PostedOn { get; set; }
        public string Company { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
    }

    internal class CompanyModel
    {
        public string Website { get; set; }
        public string Headquarters { get; set; }
        public string Size { get; set; }
        public string Founded { get;  set; }
        public string Industry { get;  set; }
        public string Description { get;  set; }
        public string Name { get;  set; }
        public string Logo { get;  set; }
        public string LogoUrl { get;  set; }
    }
}
