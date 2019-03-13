using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Scrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            program.ScrapAmbitionBox();
        }

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

        private void ScrapAmbitionBox()
        {
            var browser = new HtmlWeb();
            var interviewList = new List<InterviewModel>();

            var interviewUrls = new List<string>();

            for (int i = 0; i < 150; i++)
            {
                var url = "https://www.ambitionbox.com/interviews/companies" + "?page=" + (i + 1);
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

            foreach (var url in interviewUrls)
            {
                var doc = browser.Load(url);
                var companyName = doc.DocumentNode.SelectSingleNode("//p[contains(@class,'h1')]").InnerText.Trim();


                var interviews = doc.DocumentNode.SelectNodes("//*[@id='reviewsContainer']/article");

                foreach (var interview in interviews)
                {
                    var model = new InterviewModel
                    {
                        JobTitle = interview.SelectSingleNode(".//h2[contains(@class,'review-title')]/a").InnerText.Trim(),
                        AboutEmployee = interview.SelectSingleNode(".//div[contains(@class,'user-id')]").InnerText.Trim(),
                        PostedOn = interview.SelectSingleNode(".//div[contains(@class,'time meta-data')]//time").InnerText.Trim(),
                        Company = companyName,
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
                    foreach (var round in rounds)
                    {
                        var roundModel = new InterviewRoundModel
                        {
                            Name = round.SelectSingleNode("./h3").InnerText.Trim(),
                            Questions = new List<QuestionModel>()
                        };

                        var description = round.SelectSingleNode(".//p[contains(@class,'row_description')]");
                        if (description != null)
                            roundModel.Description = description.InnerText.Trim();
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
                    interviewList.Add(model);
                }
            }
        }
    }

    internal class InterviewModel
    {
        public string PostedOn { get; set; }
        public string AboutEmployee { get; set; }
        public string Description { get; internal set; }
        public string OverAllExp { get; set; }
        public string JobTitle { get; set; }
        public List<InterviewRoundModel> Rounds { get; set; }
        public string Company { get; internal set; }
    }

    internal class InterviewRoundModel
    {
        public string Name { get; internal set; }
        public string Description { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }

    internal class QuestionModel
    {
        public string Desc { get; set; }
    }
}
